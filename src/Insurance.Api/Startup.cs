using Insurance.Api.Config;
using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Insurance.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            RunningEnvironment.Default = env.EnvironmentName;
            RunningEnvironment.BuildVersion = "1.0";
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{RunningEnvironment.Default}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();
            
            var appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            var dbSettings = Configuration.GetSection("DbSettings");
            services.Configure<DbSettings>(dbSettings);
            services.AddSingleton<IDbSettings>(x => x.GetRequiredService<IOptions<DbSettings>>().Value);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(RunningEnvironment.BuildVersion, new OpenApiInfo{ Title = "Insurance API", Version = RunningEnvironment.BuildVersion });
            });
            
            services.AddTransient<IBusinessRules, BusinessRules>();
            services.AddSingleton<IInsuranceRepository, InsuranceRepository>();
            services.AddTransient<IInsuranceDomainService, InsuranceDomainService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddSerilog();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/" + RunningEnvironment.BuildVersion + "/swagger.json", "Insurance API " + RunningEnvironment.BuildVersion);
                });
            }
            
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            SeedDatabase.Initialize(app.ApplicationServices);
        }
    }
}

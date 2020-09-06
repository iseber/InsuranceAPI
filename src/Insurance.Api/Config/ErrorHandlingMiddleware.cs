using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace Insurance.Api.Config
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger logger = Log.ForContext<ErrorHandlingMiddleware>();

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var code = ExceptionStatusCodeMapper.MapExceptions(exception);
                if(code == HttpStatusCode.InternalServerError)
                    logger.Fatal(exception.ToString());    
                
                logger.Error(exception.ToString());
                await HandleExceptionAsync(context, exception, code);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, System.Exception exception, HttpStatusCode exceptionCode)
        {
            if (exception == null) return;
            
            await WriteExceptionAsync(context, exception, exceptionCode).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            var bodyText = JsonConvert.SerializeObject(new
            {
                error = new
                {
                    exception = exception.GetType().Name,
                    message = exception.ToString(),
                }
            });

            await response.WriteAsync(bodyText).ConfigureAwait(false);
        }
    }
}
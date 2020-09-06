using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Insurance.Tests.Configuration
{
    public class ControllerTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string) context.Request.RouteValues["id"]);
                            var testProduct = new
                                          {
                                              id = 1,
                                              name = "Test Product",
                                              productTypeId = 1,
                                              salesPrice = 750
                                          };
                            var laptopProduct = new
                                          {
                                              id = 2,
                                              name = "Laptop",
                                              productTypeId = 2,
                                              salesPrice = 450
                                          };
                            var droneProduct = new
                                          {
                                              id = 3,
                                              name = "Drone",
                                              productTypeId = 3,
                                              salesPrice = 250
                                          };
                            var tvProduct = new
                                        {
                                            id = 4,
                                            name = "Digital cameras",
                                            productTypeId = 4,
                                            salesPrice = 2500
                                        };
                            var smartphoneProduct = new
                                        {
                                            id = 5,
                                            name = "Smartphone",
                                            productTypeId = 5,
                                            salesPrice = 699
                                        };
                            var products = new [] {testProduct, laptopProduct, droneProduct, tvProduct, smartphoneProduct};
                            var product = products.First(x => x.id == productId);
                            
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Test type",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 2,
                                                       name = "Laptops",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 3,
                                                       name = "Drones",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 4,
                                                       name = "Digital Cameras",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 5,
                                                       name = "Smartphones",
                                                       canBeInsured = true
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }
}
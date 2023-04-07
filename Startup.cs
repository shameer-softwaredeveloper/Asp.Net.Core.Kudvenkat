using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        // #9  ASP NET Core appsettings json file
        //Configuration Sources --- Files (appsettings.json, appsettings.{Environment}.json), -
        //                          User Secrets, Environement variables, Command-line arguments
        //Use IConfiguration service to read configuration from different sources
        //using Microsoft.Extensions.Configuration;
        //Constructor Inject, Dependency Injection DI
        //We are using DI to inject this IConfiguration. 
        //In previous versions of ASP.NET DI was optional. To configure it we have to use external framework.
        //In ASP.NET Core DI is integral part. 
        //DI allows us to create system that are loosely coupled, extensible & easily testable  
        //Settings in Environment specific appsettings.{Environment}.json will override the settings in appsettings.json
        //IConfiguration service provided by framework will read the configuration information that is present in these -
        //different configuration sources in the order that is specified 
        //appsetttings.json --> appsettings.{Environement}.json --> User Secrets --> Environment Variables --> Command-line argument
        //override order : earlier configuration --> later configuration source
        //Later configuration sources will override the settings that is present in earlier configuration sources
        //Same key "MyKey" is present in appsettings.Development.json, launchSettings.json environemnetVariables, Command-line argument
        //If we pass a value for the same key from the command line then it should override all the previous configuration sources
        // > dotnet run MyKey="Value from CommandLine"
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/xyz", async context =>
                {
                    await context.Response.WriteAsync("Hello XYZ");
                });

                //read configuration using key "MyKey"
                endpoints.MapGet("/readconfig", async context =>
                {
                    await context.Response.WriteAsync(_config["MyKey"]);
                });
            });
        }
    }
}

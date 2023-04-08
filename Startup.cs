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

        // #10 Middleware in ASP NET Core
        // Middleware is a piece of software that can handle a HTTP request or response
        // A given middleware component has very specific purpose. Authentication, error handling, serve static files, etc middlewares
        // It is these middleware components that we use to setup a request processing pipeline
        // This pipeline determines how a request is processed
        // Request pipeline is configured as part of the application startup by Configure() method present in Startup class
        // A middleware component in ASP.NET Core has access to both incoming request & the outgoing response
        // A middleware component may process an incoming request and then pass that request to the next piece of middleware in pipeline for further processing
        // Short Circuiting : A middleware component may handle the incoming request and decide not to call the next piece of middleware in the pipeline
        // Short Circuiting is often desirable because it avoids unnecessary work. 
        // For example if the request is for a static file like an image the static files middleware can handle and serve the request and then short circuit rest of the pipeline
        // A middleware component may also simply ignore the incoming request and then pass the request on to the next piece of middleware for further processing 
        // Middleware components in the pipeline that determine how a request is processed in ASP.NET Core. 
        // These middleware components are executed in the order they are added to the pipeline. Care should be taken to add the middleware in the right order. Otherwise the application may not function as expected
        
        // Every middleware component in ASP.NET Core has access to both incoming request and outgoing response
        // A middleware component may simply pass the request to the next piece of middleware in the pipeline
        // A middleware component may do some processing and then pass the request to next middleware for further processing
        // A middleware component may handle the request and short-circuit the rest of the pipeline 
        // A middleware component may process the outgoing response
        // Middlewares are executed in the order they are added to the pipeline

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

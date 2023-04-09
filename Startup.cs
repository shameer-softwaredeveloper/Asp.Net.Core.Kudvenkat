using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        // Inject ILogger service. using Microsoft.Extensions.Logging;
        // https://github.com/dotnet/aspnetcore/blob/release/2.1/src/DefaultBuilder/src/WebHost.cs

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                // Middleware
                app.UseDeveloperExceptionPage();
            }

            // Default Files middlewear
            // app.UseDefaultFiles();

            // Overloaded Default Files middlewear
            // DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            // defaultFilesOptions.DefaultFileNames.Clear();
            // defaultFilesOptions.DefaultFileNames.Add("foo.html");  

            // default.html ---> foo.html as default page
            // app.UseDefaultFiles(defaultFilesOptions);

            // Static files middlewear
            // app.UseStaticFiles();

            // Default Files middlewear should be before Static Files middlewear. Order is important.
            // app.UseDefaultFiles();

            // Replace UseDefaultFiles() UseStaticFiles() middlewear with UseFileServer() middlewear
            // app.UseFileServer();

            // File Server middlewear overloaded
            FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");

            app.UseFileServer(fileServerOptions);

            // Middleware
            app.UseRouting();

            // Middleware
            app.UseEndpoints(endpoints =>
            {
                // MapGet has parameter RequestDelegate. RequestDelegate is a delegate that takes HttpContext object as a parameter. 
                // It is through this context object the middlewear that we are registering gains access to both the incoming http request and outgoing http response.
                // context.Request & context.Response
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

            // Order of logging request & response (1) -> (5)
            // Call next middlewear
            // app.Use(async (context, next) => 
            // {
                // await context.Response.WriteAsync("Hello from .Use() middleware 0. No endpoint matching.");
                // logger.LogInformation("Middlewear 1 incoming request"); // (1)
                // await next();
                // logger.LogInformation("Middlewear 1 outgoing response"); // (5)
            // });

            // app.Use(async (context, next) => 
            // {
                // await context.Response.WriteAsync("Hello from .Use() middleware 0. No endpoint matching.");
                // logger.LogInformation("Middlewear 2 incoming request"); // (2)
                // await next();
                // logger.LogInformation("Middlewear 2 outgoing response"); // (4)
            // });

            // Terminal middleware -- will not call next middlewear in the pipeline. Pipeline reverse from here.
            app.Run(async (context) => 
            {
                // await context.Response.WriteAsync(" Hello from .Run() Terminal middleware 1. No endpoint matching.");
                // await context.Response.WriteAsync("Middlewear 3 request handled and response produced"); // (3)
                // logger.LogInformation("Middlewear 3 request handled and response produced"); // (3)
                await context.Response.WriteAsync("Hello from Terminal middlewear");
            });

            // Terminal middleware 2 will never be execute
            // app.Run(async (context) => 
            // {
            //     await context.Response.WriteAsync("Hello from .Run() Terminal middleware 2. No endpoint matching.");
            // });
        }
    }
}

// #11 Configure ASP NET Core request processing pipeline
// wwwroot ---> all static files js, css, html, images, gif, pdf, txt are present in this special folder
// Configure(), IApplicationBuilder, app.UseEndPoints() endpoints.MapGet(), RequestDelegate, HttpContext Object
// context.Request  context.Response
// Terminal middlewear  app.Run()
// Next middlewear  app.Use()   

// ILogger, logger.LogInformation(" "),   app.Use()  await next() 
// 1.Everything that happens before the next() method is invoked in each of the middlewear components, 
// happens as the REQUEST travels from middlewear to middlewear through the pipeline
// 2.When a middlewear handles the request and produces response, the request processing pipeline starts to reverse
// 3.Everything that happens after the next() method is invoked in each of the middlewear components,
// happens as the RESPONSE travels from middlewear to middlewear through the pipeline

// #12 Static files in asp net core
// To serve static files, application should meet 2 requirements
// 1.All Static files must be present in wwwroot folder. Also called content root folder, it must be directly inside the root project folder
// 2.Static files middlewear: app.UseStaticFiles();  http://localhost:5000/webb1.jpg  http://localhost:5000/images/webb2.jpg  http://localhost:5000/foo.html
// By default UseStaticFiles() middlewear will only serve static files that are present in this wwwroot folder. 
// It is also possible to serve static files that are outside of this wwwroot folder if you want to

// Default page
// When we navigate to root url http://localhost:5000/ without any url segments, we want to serve the default document
// Name of the default page for the application should be one of the following --- default.htm, default.html, index.htm, index.html
// app.UseDefaultFiles();  wwwroot\default.html
// Default Files middlewear should be before Static Files middlewear. Order is important. Why?
// .UseDefaultFiles() doesnt actually serve the default file, it only changes the request path to point to the default document. In our case default.html
// Which then will be served by .UseStaticFiles() middlewear like any other static document. 
// If we have the order reverse then the .UseDefaultFiles middlewear will change the request path but we donot have .UseStaticFiles() middlewear next in the pipeline to serve the default file

// default.html ---> foo.html as default page
// Overloaded Default Files middlewear --- app.UseDefaultFiles(defaultFilesOptions);
// DefaultFilesOptions  .DefaultFileNames.Clear();   .DefaultFileNames.Add("foo.html");  

// UseFileServer() middlewear
// has functionality of UseDefaultFiles() UseStaticFiles() UseDirectoryBrowser() middlewear
// UseDirectoryBrowser() middlewear enables directory browsing and allows the end user to see the list of files and folders in a specified directory 

// UseFileServer() middlewear overloaded --- app.UseFileServer(fileServerOptions);
// FileServerOptions      .DefaultFilesOptions.DefaultFileNames.Clear()      .DefaultFilesOptions.DefaultFileNames.Add("foo.html");

// To add and customize these middlewear components, in most cases we add middlewear components to our applications request processing pipeline using extension method names that start with the word .Use
// Example .UseDeveloperExceptionPage()  .UseDefaultFiles()   .UseStaticFiles()   .UseFileServer()   .UseRouting()  .UseEndpoints()  .Use()  .Run()
// And Customize these middlewear components, we use the respective options objects. To customize .UseDeveloperExceptionPage() middlewear we use DeveloperExceptionPageOptions.
// Name is the same as that of the middlewear except that we have Options word appended. Simillarly to customize .UseFileServer() middlewear we use FileServerOptions object.
// To customize .UseDefaultFiles() middlewear we use DefaultFilesOptions object.

// Summary
// By default an ASP.NET Core Application will not serve static files
// The default directory for static files is wwwroot
// To serve static files UseStaticFiles() middlewear is required
// To serve default file UseDefaultFiles() middlewear is required
// default file names --- index.htm, index.html, default.htm, default.html
// but you can change the default file by customizing the behaviour of UseDefaultFiles() middlewear ie DefaultFilesOptions object
// UseDefaultFiles() must be registered before UseStaticFiles() middlewear
// UseFileServer() combines the functionality of UseStaticFiles(), UseDefaultFiles() & UseDirectoryBrowser() middleware 







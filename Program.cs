using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    //Program.cs file has Main method, which is the entry point of the application.
    //This method calls CreateHostBuilder, which calls CreateDefaultBuilder.
    //CreateDefaultBuilder sets the default order in which all these configuration sources are read 
    //https://github.com/dotnet/aspnetcore/blob/release/2.1/src/DefaultBuilder/src/WebHost.cs
    //ConfigureAppConfiguration --> order in which configuration is read. You can change the default order 
    //or you can add custom configuration source in addition to all these existing configuration sources
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

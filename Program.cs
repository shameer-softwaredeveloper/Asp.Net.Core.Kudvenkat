using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, loggingBuilder) =>
                {
                    loggingBuilder.Configure(options =>
                    {
                        options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                                            | ActivityTrackingOptions.TraceId
                                                            | ActivityTrackingOptions.ParentId;
                    });
                    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                    loggingBuilder.AddEventSourceLogger();
                    // Enable NLog as one of the Logging Provider
                    loggingBuilder.AddNLog();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

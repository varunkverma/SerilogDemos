using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
namespace SerilogDemoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger=new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Logger(level => level.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.RollingFile(@"Logs\Info-{Date}.log"))
                        .WriteTo.Logger(level => level.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo.RollingFile(@"Logs\Warnings-{Date}.log"))
                        .WriteTo.Logger(level => level.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.RollingFile(@"Logs\Errors-{Date}.log"))
                        .CreateLogger();
            try{
                CreateWebHostBuilder(args).Build().Run();
            }            
            catch(Exception ex){

            }
            finally{
                Log.CloseAndFlush();
            }
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}

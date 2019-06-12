﻿using System;
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
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        public static void Main(string[] args)
        {
            Log.Logger=new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        .CreateLogger();
            try{
                Log.Information("Host Starting");
                CreateWebHostBuilder(args).Build().Run();
            }            
            catch(Exception ex){
                Log.Fatal(ex,"Host terminated unexpectedly");
            }
            finally{
                Log.CloseAndFlush();
            }
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .UseSerilog();
    }
}

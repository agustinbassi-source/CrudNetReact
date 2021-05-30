using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bassi.API.Utilities.Configuration;
using Bassi.CrudBassi.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bassi.CrudBassi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {

                    config.AddJsonFile(".\\appsettings\\appsettings.json", false, true);
#if DEBUG
                    config.AddJsonFile(".\\appsettings\\appsettings.development.json", false, true);
#endif
#if QA
                    config.AddJsonFile(".\\appsettings\\appsettings.qa.json", false, true);
#endif
#if PRE
                    config.AddJsonFile(".\\appsettings\\appsettings.pre.json", false, true);
#endif

                    //config.AddDBConfiguration();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


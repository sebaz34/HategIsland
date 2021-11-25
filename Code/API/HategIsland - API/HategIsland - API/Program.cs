using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Obtains config file
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //Catch Nlog setup errors
                logger.Error(exception, "Program stopped at: Program.cs Main() catch()");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    });
                }).UseNLog();
    }
}

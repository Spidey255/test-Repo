



namespace CPS.Proof.DFSExtension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;    
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using Newtonsoft.Json.Serialization;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((hostingContext, config) =>
             {

                 config.SetBasePath(Directory.GetCurrentDirectory());

             })
               .ConfigureWebHostDefaults(webBuilder =>
               {

                   webBuilder.UseUrls("http://*:5011")
                   .UseStartup<Startup>();
               });
              
    }
}

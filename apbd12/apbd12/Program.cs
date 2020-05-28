using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbd12.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace apbd12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();


            using (var scope = host.Services.CreateScope() )
            {

                var services = scope.ServiceProvider;


                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception e)
                {

                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e,"An error occured seeding the Db");
                    Console.WriteLine(e);
                    throw;
                }

                host.Run();

            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var app = CreateHostBuilder(args).Build();
            using (var scope = app.Services.CreateScope())
            {
                var servises = scope.ServiceProvider;
                var logFactory = servises.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = servises.GetRequiredService<AppDbContext>();

                    await context.Database.MigrateAsync();
                    await AppContextSeed.SeedAsync(context ,logFactory);
                }
                catch(Exception ex)
                {
                    var logger = logFactory.CreateLogger<Program>();
                    logger.LogError(ex, "error occured when trying add migration");
                }
            }
          await  app.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApplicationCore.Services;
using AutoMapper;
using ApplicationCore.Entities;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var greenairContext = services.GetRequiredService<GreenairContext>();

                    IUnitOfWork unit = new UnitOfWork(greenairContext);
                    
                    unit.Flights.RemoveRange(unit.Flights.GetAll());
                    
                    //unit.Employees.RemoveRange(unit.Employees.GetAll());
                    //unit.Customers.RemoveRange(unit.Customers.GetAll());


                    // var acc = unit.Accounts.GetByAsync("cus1").GetAwaiter().GetResult();
                    // //acc.Username = "customer1";
                    // acc.Password = "12345";
                    // Console.WriteLine(acc.PersonId);
                    // unit.Accounts.UpdateAsync(acc);
                    //unit.Flights.RemoveAsync(unit.Flights.GetByAsync("00006").GetAwaiter().GetResult());


                    greenairContext.SaveChanges();
                    DataSeed.Initialize(greenairContext);


                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Database error occured when putting data.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
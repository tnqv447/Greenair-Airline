using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repos;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ApplicationCore.Services;
using ApplicationCore;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Presentation.Services.ServiceInterfaces;
using Presentation.Services.ServicesImplement;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<GreenairContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("Greenair"), x => x.MigrationsAssembly("Presentation")));
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                // You might want to only set the application cookies over a secure connection:
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            services.AddDistributedMemoryCache();
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddAntiforgery(o =>
            {
                o.HeaderName = "XSRF-TOKEN";
            });
            // services.AddMvc();
            // Mapping
            services.AddAutoMapper(typeof(Mapping));
            // Repositories
            services.AddScoped<IMakerRepository, MakerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPlaneRepository, PlaneRepository>();


            // Web Services

            // Services
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPlaneService, PlaneService>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ITicketTypeService, TicketTypeService>();
            services.AddScoped<IMakerService, MakerService>();
            //.....................
            services.AddScoped<IAirportVMService, AirportVMService>();
            services.AddScoped<IPlaneVMService, PlaneVMService>();
            services.AddScoped<IMakerVMService, MakerVMService>();
            services.AddScoped<ICustomerVMService, CustomerVMService>();
            services.AddScoped<IEmployeeVMService, EmployeeVMService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
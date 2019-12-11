
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Configuration;
using System.Threading.Tasks;
using System;

namespace Infrastructure.Persistence
{
    public class GreenairContext : DbContext
    {
        public GreenairContext(DbContextOptions<GreenairContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AirportConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new MakerConfiguration());
            modelBuilder.ApplyConfiguration(new PlaneConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new FlightDetailConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
        }


        public DbSet<Maker> Makers { get; set; }
        public DbSet<Plane> Planes { get; set; }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightDetail> FlightDetails { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }

}
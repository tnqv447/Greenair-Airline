using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure.Persistence.Repos;
namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GreenairContext context;

        public UnitOfWork(GreenairContext _context)
        {
            this.Accounts = new AccountRepository(_context);
            this.Airports = new AirportRepository(_context);
            this.Persons = new PersonRepository(_context);
            this.Customers = new CustomerRepository(_context);
            this.Employees = new EmployeeRepository(_context);
            this.Flights = new FlightRepository(_context);
            this.Jobs = new JobRepository(_context);
            this.Makers = new MakerRepository(_context);
            this.Planes = new PlaneRepository(_context);
            this.Routes = new RouteRepository(_context);
            this.TicketTypes = new TicketTypeRepository(_context);
            context = _context;

        }
        public IAccountRepository Accounts { get; private set; }

        public IAirportRepository Airports { get; private set; }

        public IPersonRepository Persons { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public IEmployeeRepository Employees { get; private set; }

        public IFlightRepository Flights { get; private set; }

        public IJobRepository Jobs { get; private set; }

        public IMakerRepository Makers { get; private set; }

        public IPlaneRepository Planes { get; private set; }

        public IRouteRepository Routes { get; private set; }

        public ITicketTypeRepository TicketTypes { get; private set; }


        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
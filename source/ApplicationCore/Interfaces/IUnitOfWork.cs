using System.Threading.Tasks;
using System;
namespace ApplicationCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IAirportRepository Airports { get; }
        IPersonRepository Persons { get; }
        ICustomerRepository Customers { get; }
        IEmployeeRepository Employees { get; }
        IFlightRepository Flights { get; }
        IJobRepository Jobs { get; }
        IMakerRepository Makers { get; }
        IPlaneRepository Planes { get; }
        IRouteRepository Routes { get; }
        ITicketTypeRepository TicketTypes { get; }

        Task<int> CompleteAsync();
    }
}
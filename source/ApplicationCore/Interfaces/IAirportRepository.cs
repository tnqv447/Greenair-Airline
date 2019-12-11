using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
namespace ApplicationCore.Interfaces
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<IEnumerable<Airport>> getAirportByConditions(string airport_name, string city, string country);
        Task<bool> isDomestic(string airport_id);

        Task<IEnumerable<Airport>> getAvailableAirport();
        Task<IEnumerable<Airport>> getDisabledAirport();

        Task disable(string airport_id);
        Task activate(string aiport_id);
        Task disable(Airport acc);
        Task activate(Airport acc);
    }
}
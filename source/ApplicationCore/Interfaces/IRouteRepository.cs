using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
namespace ApplicationCore.Interfaces
{
    public interface IRouteRepository : IRepository<Route>
    {
        Task<IEnumerable<Route>> getRouteByOriginAsync(string origin);
        Task<IEnumerable<Route>> getRouteByDestinationAsync(string destination);
        Task<bool> isExisted(Route route);
        Task<bool> isExisted(string origin, string dest);

        Task<IEnumerable<Route>> getAvailableRoute();
        Task<IEnumerable<Route>> getDisabledRoute();

        Task disable(string Route_id);
        Task activate(string Route_id);
        Task disable(Route acc);
        Task activate(Route acc);
    }
}
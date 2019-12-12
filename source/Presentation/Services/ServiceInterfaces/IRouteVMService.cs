using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using Presentation.ViewModels;
namespace Presentation.Services.ServiceInterfaces
{
    public interface IRouteVMService
    {
        Task<RoutePageVM> GetRoutePageViewModelAsync(string searchString, int pageIndex = 1);
        Task<IEnumerable<AirportDTO>> GetAllAirport();
        Task<RouteDTO> GetRoute(string route_id);
        Task UpdateRoute(RouteDTO route);
        Task AddRoute(RouteDTO route);
        Task RemoveRoute(string route_id);
    }
}
using System.Threading.Tasks;
using System.Collections.Generic;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.ViewModels;
using Presentation.Services.ServiceInterfaces;
using AutoMapper;
using ApplicationCore.Services;
using ApplicationCore;

namespace Presentation.Services.ServicesImplement
{
    public class RouteVMService : IRouteVMService
    {
        private int pageSize = 3;
        private readonly IRouteService _service;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;
        // public IList<RouteVM> ListRoutes { get; set; }
        public RouteVMService(IRouteService service, IMapper mapper, IAirportService airportService)
        {
            _service = service;
            _mapper = mapper;
            _airportService = airportService;
        }

        public async Task<RoutePageVM> GetRoutePageViewModelAsync(string searchString, int pageIndex = 1)
        {
            List<RouteVM> ListRoutes = new List<RouteVM>();
            // var movies = await _service.GetMoviesAsync(searchString, genre);
            var Routes = await _service.getAllRouteAsync();
            var origin_name = "";
            var destination_name = "";
            Routes = await _service.SortAsync(Routes,ORDER_ENUM.ORIGIN_NAME,ORDER_ENUM.DESCENDING);
            foreach(var item in Routes)
            {
                AirportDTO origin_airport = await _airportService.getAirportAsync(item.Origin);
                AirportDTO des_airport = await _airportService.getAirportAsync(item.Destination);
                origin_name = origin_airport.AirportName;
                destination_name = des_airport.AirportName;
                RouteVM routeVM = new RouteVM(item.RouteId,origin_name,destination_name,item.Origin,item.Destination,item.Status,item.FlightTime.toString());
                ListRoutes.Add(routeVM);
            }
            // if (searchString != null)
            // {
            //     // Routes = await _service.getRouteByConditionsAsync(searchString, "", "");
            // }
            // var genres = await _service.GetGenresAsync();
            // var abc = _mapper.Map<IEnumerable<Route>, IEnumerable<RouteDTO>>(Routes);

            return new RoutePageVM
            {
                Routes = PaginatedList<RouteVM>.Create(ListRoutes, pageIndex, pageSize)
            };
        }
        public async Task<IEnumerable<AirportDTO>> GetAllAirport()
        {
            return await _airportService.getAllAirportAsync();
        } 
        public async Task<RouteDTO> GetRoute(string route_id)
        {
            return await _service.getRouteAsync(route_id);
        }
        public async Task UpdateRoute(RouteDTO route)
        {
            await _service.updateRouteAsync(route);
        }
        public async Task AddRoute(RouteDTO route)
        {
            await _service.addRouteAsync(route);
        }
        public async Task RemoveRoute(string route_id)
        {
            await _service.removeRouteAsync(route_id);
        }
    }   
}
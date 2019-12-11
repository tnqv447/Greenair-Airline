using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using System;
namespace ApplicationCore.Services
{
    public class RouteService : Service<Route, RouteDTO, RouteDTO>, IRouteService
    {
        // public IEnumerable<Route> List { get; set; }
        public RouteService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            // List = unitOfWork.Routes.GetAllAsync().GetAwaiter().GetResult();
        }
        //query
        public async Task<RouteDTO> getRouteAsync(string route_id)
        {
            return this.toDto(await unitOfWork.Routes.GetByAsync(route_id));
        }
        public async Task<IEnumerable<RouteDTO>> getAllRouteAsync()
        {
            return this.toDtoRange(await unitOfWork.Routes.GetAllAsync());
        }
        public async Task<IEnumerable<RouteDTO>> getRouteByOriginAsync(string origin)
        {
            return this.toDtoRange(await unitOfWork.Routes.getRouteByOriginAsync(origin));
        }
        public async Task<IEnumerable<RouteDTO>> getRouteByDestinationAsync(string destination)
        {
            return this.toDtoRange(await unitOfWork.Routes.getRouteByDestinationAsync(destination));
        }

        public async Task<IEnumerable<RouteDTO>> getAvailableRouteAsync()
        {
            var Routes = await unitOfWork.Routes.getAvailableRoute();
            return this.toDtoRange(Routes);
        }
        public async Task<IEnumerable<RouteDTO>> getDisabledRouteAsync()
        {
            var Routes = await unitOfWork.Routes.getDisabledRoute();
            return this.toDtoRange(Routes);
        }

        new public async Task<IEnumerable<Route>> SortAsync(IEnumerable<Route> entities, ORDER_ENUM col, ORDER_ENUM order)
        {
            IEnumerable<Route> res = null;
            await Task.Run(() => true);
            if (order == ORDER_ENUM.DESCENDING)
            {
                switch (col)
                {
                    case ORDER_ENUM.ORIGIN_NAME: res = entities.OrderByDescending(m => unitOfWork.Airports.GetByAsync(m.Origin).GetAwaiter().GetResult().AirportName); break;
                    case ORDER_ENUM.DESTINATION_NAME: res = entities.OrderByDescending(m => unitOfWork.Airports.GetByAsync(m.Destination).GetAwaiter().GetResult().AirportName); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderByDescending(m => m.Status); break;
                    default: res = entities.OrderByDescending(m => m.RouteId); break;
                }
            }
            else
            {
                switch (col)
                {
                    case ORDER_ENUM.ORIGIN_NAME: res = entities.OrderBy(m => unitOfWork.Airports.GetByAsync(m.Origin).GetAwaiter().GetResult().AirportName); break;
                    case ORDER_ENUM.DESTINATION_NAME: res = entities.OrderBy(m => unitOfWork.Airports.GetByAsync(m.Destination).GetAwaiter().GetResult().AirportName); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderBy(m => m.Status); break;
                    default: res = entities.OrderBy(m => m.RouteId); break;
                }

            }
            return res;
        }

        //actions
        private async Task generateRouteId(Route Route)
        {
            var res = await unitOfWork.Routes.GetAllAsync();
            res = res.OrderBy(m => m.RouteId);
            string id = null;
            if (res != null) id = res.LastOrDefault().RouteId;
            var code = 0;
            Int32.TryParse(id, out code);
            Route.RouteId = String.Format("{0:00000}", code + 1);
        }
        private async Task<bool> isExisted(Route route)
        {
            return await unitOfWork.Routes.isExisted(route);
        }
        public async Task addRouteAsync(RouteDTO dto)
        {
            var res = await unitOfWork.Routes.GetByAsync(dto.RouteId);
            if (res == null && !await this.isExisted(this.toEntity(dto)))
            {
                var route = this.toEntity(dto);
                await this.generateRouteId(route);
                await unitOfWork.Routes.AddAsync(route);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task removeRouteAsync(string route_id)
        {
            var route = await unitOfWork.Routes.GetByAsync(route_id);
            if (route != null)
            {
                await unitOfWork.Routes.RemoveAsync(route);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task updateRouteAsync(RouteDTO dto)
        {
            if (await unitOfWork.Routes.GetByAsync(dto.RouteId) != null && await this.isExisted(this.toEntity(dto)))
            {
                // var Route = this.toEntity(dto);
                // await unitOfWork.Routes.UpdateAsync(Route);
                var Route = await unitOfWork.Routes.GetByAsync(dto.RouteId);
                this.convertDtoToEntity(dto, Route);
            }
            else
            {
                var route = this.toEntity(dto);
                await this.generateRouteId(route);
                await unitOfWork.Routes.AddAsync(route);
            }
            await unitOfWork.CompleteAsync();
        }

        public async Task disableRouteAsync(string cus_id)
        {
            var cus = await unitOfWork.Routes.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Routes.disable(cus);
        }
        public async Task activateRouteAsync(string cus_id)
        {
            var cus = await unitOfWork.Routes.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Routes.activate(cus);
        }
    }
}
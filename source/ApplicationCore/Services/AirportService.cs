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
    public class AirportService : Service<Airport, AirportDTO, AirportDTO>, IAirportService
    {
        //public IEnumerable<Airport> List { get; set; }
        public AirportService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            // List = unitOfWork.Airports.GetAllAsync().GetAwaiter().GetResult();
        }
        //query
        public async Task<AirportDTO> getAirportAsync(string airport_id)
        {
            return this.toDto(await unitOfWork.Airports.GetByAsync(airport_id));
        }
        public async Task<IEnumerable<Object>> searchAirport(string term)
        {
            var airports = await this.getAllAirportAsync();

            if(term != null){
                var res = airports.Where(t =>
                                t.Address.City.ToLower().StartsWith(term.ToLower())).Select(t => new { Name = t.Address.City, Id = t.AirportId }
                                ).ToList();
                return res;
            }
            else{
                var res = airports.Select(t => new { Name = t.Address.City, Id = t.AirportId }).ToList();
                return res;
            }
        }
        public async Task<IEnumerable<Object>> getAirportName()
        {
            var airports = await this.getAllAirportAsync();
            var res = airports.Select(t => new { Name = t.AirportName, Id = t.AirportId }
                                ).ToList();
            return res;
        }
        public async Task<IEnumerable<AirportDTO>> getAllAirportAsync()
        {
            return this.toDtoRange(await unitOfWork.Airports.GetAllAsync());
        }
        public async Task<IEnumerable<AirportDTO>> getAirportByConditionsAsync(string airport_name, string city, string country)
        {
            return this.toDtoRange(await unitOfWork.Airports.getAirportByConditions(airport_name, city, country));
        }
        public async Task<bool> isDomestic(string airport_id)
        {
            return await unitOfWork.Airports.isDomestic(airport_id);
        }

        public async Task<IEnumerable<AirportDTO>> getAvailableAirportAsync()
        {
            var Airports = await unitOfWork.Airports.getAvailableAirport();
            return this.toDtoRange(Airports);
        }
        public async Task<IEnumerable<AirportDTO>> getDisabledAirportAsync()
        {
            var Airports = await unitOfWork.Airports.getDisabledAirport();
            return this.toDtoRange(Airports);
        }

        new public async Task<IEnumerable<Airport>> SortAsync(IEnumerable<Airport> entities, ORDER_ENUM col, ORDER_ENUM order)
        {
            IEnumerable<Airport> res = null;
            await Task.Run(() => true);
            if (order == ORDER_ENUM.DESCENDING)
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderByDescending(m => m.AirportName); break;
                    case ORDER_ENUM.ADDRESS: res = entities.OrderByDescending(m => m.Address.ToString()); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderByDescending(m => m.Status); break;
                    default: res = entities.OrderByDescending(m => m.AirportId); break;
                }
            }
            else
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderBy(m => m.AirportName); break;
                    case ORDER_ENUM.ADDRESS: res = entities.OrderBy(m => m.Address.ToString()); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderBy(m => m.Status); break;
                    default: res = entities.OrderBy(m => m.AirportId); break;
                }

            }
            return res;
        }




        //actions
        public async Task addAirportAsync(AirportDTO dto)
        {
            if (await unitOfWork.Airports.GetByAsync(dto.AirportId) == null)
            {
                var airport = this.toEntity(dto);
                await unitOfWork.Airports.AddAsync(airport);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task removeAirportAsync(string airport_id)
        {
            var airport = await unitOfWork.Airports.GetByAsync(airport_id);
            if (airport != null)
            {
                await unitOfWork.Airports.RemoveAsync(airport);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task updateAirportAsync(AirportDTO dto)
        {
            if (await unitOfWork.Airports.GetByAsync(dto.AirportId) != null)
            {
                // var airport = this.toEntity(dto);
                // await unitOfWork.Airports.UpdateAsync(airport);
                var airport = await unitOfWork.Airports.GetByAsync(dto.AirportId);
                this.convertDtoToEntity(dto, airport);
            }
            else
            {
                var airport = this.toEntity(dto);
                await unitOfWork.Airports.AddAsync(airport);
            }
            await unitOfWork.CompleteAsync();
        }

        public async Task disableAirportAsync(string cus_id)
        {
            var cus = await unitOfWork.Airports.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Airports.disable(cus);
        }
        public async Task activateAirportAsync(string cus_id)
        {
            var cus = await unitOfWork.Airports.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Airports.activate(cus);
        }
    }
}
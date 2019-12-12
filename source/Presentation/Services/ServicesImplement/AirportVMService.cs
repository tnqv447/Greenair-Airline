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

namespace Presentation.Services.ServicesImplement
{
    public class AirportVMService : IAirportVMService
    {
        private int pageSize = 3;
        private readonly IAirportService _service;
        private readonly IMapper _mapper;

        public AirportVMService(IAirportService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<AirportPageVM> GetAirportPageViewModelAsync(string searchString, int pageIndex = 1)
        {
            // var movies = await _service.GetMoviesAsync(searchString, genre);
            var airports = await _service.getAvailableAirportAsync();
            if (searchString != null)
            {
                airports = await _service.getAirportByConditionsAsync(searchString, "", "");
            }
            // var genres = await _service.GetGenresAsync();
            // var abc = _mapper.Map<IEnumerable<Airport>, IEnumerable<AirportDTO>>(airports);

            return new AirportPageVM
            {
                Airports = PaginatedList<AirportDTO>.Create(airports, pageIndex, pageSize)
            };
        }

    }
}
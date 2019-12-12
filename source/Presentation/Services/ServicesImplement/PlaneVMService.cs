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
    public class PlaneVMService : IPlaneVMService
    {
        private int pageSize = 3;
        private readonly IPlaneService _service;
        private readonly IMapper _mapper;

        public PlaneVMService(IPlaneService movieService, IMapper mapper)
        {
            _service = movieService;
            _mapper = mapper;
        }

        public async Task<PlanePageVM> GetPlanePageViewModelAsync(string searchString, int pageIndex = 1)
        {
            // var movies = await _service.GetMoviesAsync(searchString, genre);
            var Planes = await _service.getAvailablePlaneAsync();
            if (searchString != null)
            {
                // var makerName = _service.Makers.get;
                Planes = await _service.getPlaneByMakerIdAsync(searchString.Substring(0, 3));
            }
            Planes = await _service.SortAsync(Planes, ApplicationCore.ORDER_ENUM.ID, ApplicationCore.ORDER_ENUM.ASCENDING);

            return new PlanePageVM
            {
                Planes = PaginatedList<PlaneDTO>.Create(Planes, pageIndex, pageSize)
            };
        }
    }
}
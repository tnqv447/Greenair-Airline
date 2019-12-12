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
    public class MakerVMService : IMakerVMService
    {
        private int pageSize = 3;
        private readonly IMakerService _service;
        private readonly IMapper _mapper;

        public MakerVMService(IMakerService movieService, IMapper mapper)
        {
            _service = movieService;
            _mapper = mapper;
        }

        public async Task<MakerPageVM> GetMakerPageViewModelAsync(string searchString, int pageIndex = 1)
        {
            // var movies = await _service.GetMoviesAsync(searchString, genre);
            var Makers = await _service.getAvailableMakerAsync();
            if (searchString != null)
            {
                Makers = await _service.getMakerByNameAsync(searchString);
            }
            // var genres = await _service.GetGenresAsync();
            Makers = await _service.SortAsync(Makers, ApplicationCore.ORDER_ENUM.ID, ApplicationCore.ORDER_ENUM.ASCENDING);

            return new MakerPageVM
            {
                Makers = PaginatedList<MakerDTO>.Create(Makers, pageIndex, pageSize)
            };
        }
        public async Task RemoveMaker(string id)
        {
            await _service.disableMakerAsync(id);
        }

    }
}
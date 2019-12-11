using System.Threading.Tasks;
using System.Collections.Generic;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.ViewModels;
using Presentation.Services.ServiceInterfaces;
using AutoMapper;
namespace Presentation.Services.ServicesImplement
{
    public class MakerVMService : IMakerVMService
    {
        private int pageSize = 3;
        private readonly IUnitOfWork _service;
        private readonly IMapper _mapper;

        public MakerVMService(IUnitOfWork movieService, IMapper mapper)
        {
            _service = movieService;
            _mapper = mapper;
        }

        public async Task<MakerPageVM> GetMakerPageViewModelAsync(string searchString, int pageIndex = 1)
        {
            // var movies = await _service.GetMoviesAsync(searchString, genre);
            var Makers = await _service.Makers.GetAllAsync();
            if (searchString != null)
            {
                Makers = await _service.Makers.getMakerByName(searchString);
            }
            // var genres = await _service.GetGenresAsync();
            var abc = _mapper.Map<IEnumerable<Maker>, IEnumerable<MakerDTO>>(Makers);

            return new MakerPageVM
            {
                Makers = PaginatedList<MakerDTO>.Create(abc, pageIndex, pageSize)
            };
        }

    }
}
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
    public class CustomerVMService : ICustomerVMService
    {
        private int pageSize = 3;
        private readonly ICustomerService service;
        private readonly IMapper _mapper;

        public CustomerVMService(ICustomerService movieService, IMapper mapper)
        {
            this.service = movieService;
            _mapper = mapper;
        }

        public async Task<CustomerPageVM> GetCustomerPageViewModelAsync(string searchString, int pageIndex = 1)
        {
            // var movies = await _service.GetMoviesAsync(searchString, genre);
            var Customers = await service.getAllCustomerAsync();
            // var genres = await _service.GetGenresAsync();
            if (searchString != null)
            {
                Customers = await service.getCustomerByName(searchString);
            }
            // var abc = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(Customers);

            return new CustomerPageVM
            {
                Customers = PaginatedList<CustomerDTO>.Create(Customers, pageIndex, pageSize)
            };
        }

    }
}
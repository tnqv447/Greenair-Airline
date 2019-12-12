using System;
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
    public class FlightVMService : IFlightVMService
    {
        private int pageSize = 3;
        // private readonly IFlightService service;
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly IFlightService _service;

        // public FlightVMService(IFlightService movieService, IMapper mapper)
        // {
        //     this.service = movieService;
        //     _mapper = mapper;
        // }
        public FlightVMService(IFlightService service, IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _service = service;
        }

        public async Task<FlightPageVM> GetFlightPageViewModelAsync(IEnumerable<FlightDTO> list, DateTime depDate, DateTime arrDate, string searchString, int pageIndex = 1)
        {
            // var Flights = await service.getAllFlightAsync();


            if (searchString != null)
            {
                DateTime value_date = DateTime.ParseExact(searchString, "dd/MM/yyyy", null);
                list = await _service.searchFlightAsync(null, null, value_date, 0, 0);
            }
            list = await _service.SortAsync(list, ORDER_ENUM.ID, ORDER_ENUM.DESCENDING);
            return new FlightPageVM
            {
                Flights = PaginatedList<FlightDTO>.Create(list, pageIndex, pageSize)
            };
        }

    }
}
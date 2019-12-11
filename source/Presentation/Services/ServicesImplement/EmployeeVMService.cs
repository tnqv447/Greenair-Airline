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
    public class EmployeeVMService : IEmployeeVMService
    {
        private int pageSize = 3;
        // private readonly IEmployeeService service;
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        // public EmployeeVMService(IEmployeeService movieService, IMapper mapper)
        // {
        //     this.service = movieService;
        //     _mapper = mapper;
        // }
        public EmployeeVMService(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<EmployeePageVM> GetEmployeePageViewModelAsync(string searchString, int pageIndex = 1)
        {
            // var Employees = await service.getAllEmployeeAsync();
            var Employees = await _unitofwork.Employees.GetAllAsync();
            if (searchString != null)
            {
                // Employees = await service.getEmployeeByName(searchString);
            }
            var abc = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(Employees);

            return new EmployeePageVM
            {
                Employees = PaginatedList<EmployeeDTO>.Create(abc, pageIndex, pageSize)
            };
        }

    }
}
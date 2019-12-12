using System.Threading.Tasks;
using System.Collections.Generic;
using Presentation.ViewModels;
using ApplicationCore.DTOs;
using System;
namespace Presentation.Services.ServiceInterfaces
{
    public interface IFlightVMService
    {
        Task<FlightPageVM> GetFlightPageViewModelAsync(IEnumerable<FlightDTO> list, DateTime depDate, DateTime arrDate, string searchString, int pageIndex = 1);
    }
}
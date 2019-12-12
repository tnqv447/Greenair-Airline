using Presentation;
using ApplicationCore.DTOs;
namespace Presentation.ViewModels
{
    public class FlightPageVM
    {
        public PaginatedList<FlightDTO> Flights { get; set; }
    }
}
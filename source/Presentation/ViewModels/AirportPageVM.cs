using Presentation;
using ApplicationCore.DTOs;
namespace Presentation.ViewModels
{
    public class AirportPageVM
    {
        public PaginatedList<AirportDTO> Airports { get; set; }
    }
}
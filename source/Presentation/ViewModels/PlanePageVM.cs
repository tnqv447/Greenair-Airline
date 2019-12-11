using Presentation;
using ApplicationCore.DTOs;
namespace Presentation.ViewModels
{
    public class PlanePageVM
    {
        public PaginatedList<PlaneDTO> Planes { get; set; }
    }
}
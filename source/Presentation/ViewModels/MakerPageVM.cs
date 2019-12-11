using Presentation;
using ApplicationCore.DTOs;
namespace Presentation.ViewModels
{
    public class MakerPageVM
    {
        public PaginatedList<MakerDTO> Makers { get; set; }
    }
}
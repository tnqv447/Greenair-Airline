using Presentation;
using ApplicationCore.DTOs;
namespace Presentation.ViewModels
{
    public class EmployeePageVM
    {
        public PaginatedList<EmployeeDTO> Employees { get; set; }
    }
}
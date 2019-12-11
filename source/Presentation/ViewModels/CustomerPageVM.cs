using Presentation;
using ApplicationCore.DTOs;
namespace Presentation.ViewModels
{
    public class CustomerPageVM
    {
        public PaginatedList<CustomerDTO> Customers { get; set; }
    }
}
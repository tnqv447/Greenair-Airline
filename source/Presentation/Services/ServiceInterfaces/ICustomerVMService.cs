using System.Threading.Tasks;
using Presentation.ViewModels;
namespace Presentation.Services.ServiceInterfaces
{
    public interface ICustomerVMService
    {
        Task<CustomerPageVM> GetCustomerPageViewModelAsync(string searchString, int pageIndex = 1);
    }
}
using System.Threading.Tasks;
using Presentation.ViewModels;
namespace Presentation.Services.ServiceInterfaces
{
    public interface IEmployeeVMService
    {
        Task<EmployeePageVM> GetEmployeePageViewModelAsync(string searchString, int pageIndex = 1);
    }
}
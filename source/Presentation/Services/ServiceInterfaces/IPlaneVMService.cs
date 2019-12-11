using System.Threading.Tasks;
using Presentation.ViewModels;
namespace Presentation.Services.ServiceInterfaces
{
    public interface IPlaneVMService
    {
        Task<PlanePageVM> GetPlanePageViewModelAsync(string searchMakerId, int pageIndex = 1);
    }
}
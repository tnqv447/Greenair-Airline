using System.Threading.Tasks;
using Presentation.ViewModels;
namespace Presentation.Services.ServiceInterfaces
{
    public interface IMakerVMService
    {
        Task<MakerPageVM> GetMakerPageViewModelAsync(string searchString, int pageIndex = 1);
    }
}
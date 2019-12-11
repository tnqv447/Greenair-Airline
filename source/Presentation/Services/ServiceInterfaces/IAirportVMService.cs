using System.Threading.Tasks;
using Presentation.ViewModels;
namespace Presentation.Services.ServiceInterfaces
{
    public interface IAirportVMService
    {
        Task<AirportPageVM> GetAirportPageViewModelAsync(string searchString, int pageIndex = 1);
    }
}
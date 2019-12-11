using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
namespace ApplicationCore.Interfaces
{
    public interface IPlaneRepository : IRepository<Plane>
    {
        Task<IEnumerable<Plane>> getPlanebyMakerId(string maker_id);

        Task<IEnumerable<Plane>> getAvailablePlane();
        Task<IEnumerable<Plane>> getDisabledPlane();
        
        Task disable(string Plane_id);
        Task activate(string Plane_id);
        Task disable(Plane acc);
        Task activate(Plane acc);
    }
}
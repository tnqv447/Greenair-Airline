using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.DTOs;
namespace ApplicationCore.Services
{
    public interface IPlaneService : IService<Plane, PlaneDTO, PlaneDTO>
    {
        //query
        Task<PlaneDTO> getPlaneAsync(string plane_id);
        Task<IEnumerable<PlaneDTO>> getAllPlaneAsync();
        Task<IEnumerable<PlaneDTO>> getPlaneByMakerIdAsync(string maker_id);
        Task<String> getPlaneFullname(string plane_id);
        Task<String> getPlaneFullname(Plane plane, string maker_id);

        Task<IEnumerable<PlaneDTO>> getAvailablePlaneAsync();
        Task<IEnumerable<PlaneDTO>> getDisabledPlaneAsync();

        //actions
        Task addPlaneAsync(PlaneDTO dto);
        Task removePlaneAsync(string plane_id);
        Task updatePlaneAsync(PlaneDTO dto);

        Task disablePlaneAsync(string Plane_id);
        Task activatePlaneAsync(string Plane_id);
    }
}
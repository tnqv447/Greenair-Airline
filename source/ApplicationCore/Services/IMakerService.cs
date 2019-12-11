using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.DTOs;
namespace ApplicationCore.Services
{
    public interface IMakerService : IService<Maker, MakerDTO, MakerDTO>
    {
        //query
        Task<MakerDTO> getMakerAsync(string maker_id);
        Task<IEnumerable<MakerDTO>> getAllMakerAsync();
        Task<IEnumerable<MakerDTO>> getMakerByNameAsync(string maker_name);

        Task<IEnumerable<MakerDTO>> getAvailableMakerAsync();
        Task<IEnumerable<MakerDTO>> getDisabledMakerAsync();

        //actions
        Task addMakerAsync(MakerDTO dto);
        Task removeMakerAsync(string Maker_id);
        Task updateMakerAsync(MakerDTO dto);

        Task disableMakerAsync(string Maker_id);
        Task activateMakerAsync(string Maker_id);
    }
}
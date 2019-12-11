using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using System;
namespace ApplicationCore.Services
{
    public class MakerService : Service<Maker, MakerDTO, MakerDTO>, IMakerService
    {
        // public IEnumerable<Maker> List { get; set; }
        public MakerService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            // List = unitOfWork.Makers.GetAllAsync().GetAwaiter().GetResult();
        }
        //query
        public async Task<MakerDTO> getMakerAsync(string Maker_id)
        {
            return this.toDto(await unitOfWork.Makers.GetByAsync(Maker_id));
        }
        public async Task<IEnumerable<MakerDTO>> getAllMakerAsync()
        {
            return this.toDtoRange(await unitOfWork.Makers.GetAllAsync());
        }
        public async Task<IEnumerable<MakerDTO>> getMakerByNameAsync(string Maker_name)
        {
            return this.toDtoRange(await unitOfWork.Makers.getMakerByName(Maker_name));
        }

        public async Task<IEnumerable<MakerDTO>> getAvailableMakerAsync()
        {
            var Makers = await unitOfWork.Makers.getAvailableMaker();
            return this.toDtoRange(Makers);
        }
        public async Task<IEnumerable<MakerDTO>> getDisabledMakerAsync()
        {
            var Makers = await unitOfWork.Makers.getDisabledMaker();
            return this.toDtoRange(Makers);
        }

        new public async Task<IEnumerable<Maker>> SortAsync(IEnumerable<Maker> entities, ORDER_ENUM col, ORDER_ENUM order)
        {
            IEnumerable<Maker> res = null;
            await Task.Run(() => true);
            if (order == ORDER_ENUM.DESCENDING)
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderByDescending(m => m.MakerName); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderByDescending(m => m.Status); break;
                    case ORDER_ENUM.ADDRESS: res = entities.OrderByDescending(m => m.Address.ToString()); break;
                    default: res = entities.OrderByDescending(m => m.MakerId); break;
                }
            }
            else
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderBy(m => m.MakerName); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderBy(m => m.Status); break;
                    case ORDER_ENUM.ADDRESS: res = entities.OrderBy(m => m.Address.ToString()); break;
                    default: res = entities.OrderBy(m => m.MakerId); break;
                }

            }
            return res;
        }

        //actions
        private async Task generateMakerId(Maker Maker)
        {
            var res = await unitOfWork.Makers.GetAllAsync();
            res = res.OrderBy(m => m.MakerId);
            string id = null;
            if (res != null) id = res.LastOrDefault().MakerId;
            var code = 0;
            Int32.TryParse(id, out code);
            Maker.MakerId = String.Format("{0:000}", code + 1);
        }
        public async Task addMakerAsync(MakerDTO dto)
        {
            if (await unitOfWork.Makers.GetByAsync(dto.MakerId) == null)
            {
                var Maker = this.toEntity(dto);
                await this.generateMakerId(Maker);
                await unitOfWork.Makers.AddAsync(Maker);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task removeMakerAsync(string Maker_id)
        {
            var Maker = await unitOfWork.Makers.GetByAsync(Maker_id);
            if (Maker != null)
            {
                await unitOfWork.Makers.RemoveAsync(Maker);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task updateMakerAsync(MakerDTO dto)
        {
            if (await unitOfWork.Makers.GetByAsync(dto.MakerId) != null)
            {
                // var Maker = this.toEntity(dto);
                // await unitOfWork.Makers.UpdateAsync(Maker);
                var Maker = await unitOfWork.Makers.GetByAsync(dto.MakerId);
                this.convertDtoToEntity(dto, Maker);
            }
            else
            {
                var Maker = this.toEntity(dto);
                await this.generateMakerId(Maker);
                await unitOfWork.Makers.AddAsync(Maker);
            }
            await unitOfWork.CompleteAsync();
        }

        public async Task disableMakerAsync(string cus_id)
        {
            var cus = await unitOfWork.Makers.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Makers.disable(cus);
        }
        public async Task activateMakerAsync(string cus_id)
        {
            var cus = await unitOfWork.Makers.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Makers.activate(cus);
        }
    }
}
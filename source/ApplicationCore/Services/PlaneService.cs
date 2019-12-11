using System.Globalization;
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
    public class PlaneService : Service<Plane, PlaneDTO, PlaneDTO>, IPlaneService
    {
        // public IEnumerable<Plane> List { get; set; }
        public PlaneService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            // List = unitOfWork.Planes.GetAllAsync().GetAwaiter().GetResult();
        }
        //query
        public async Task<PlaneDTO> getPlaneAsync(string plane_id)
        {
            return this.toDto(await unitOfWork.Planes.GetByAsync(plane_id));
        }
        public async Task<IEnumerable<PlaneDTO>> getAllPlaneAsync()
        {
            return this.toDtoRange(await unitOfWork.Planes.GetAllAsync());
        }
        public async Task<IEnumerable<PlaneDTO>> getPlaneByMakerIdAsync(string maker_id)
        {
            return this.toDtoRange(await unitOfWork.Planes.getPlanebyMakerId(maker_id));
        }
        public async Task<String> getPlaneFullname(string plane_id)
        {
            var plane = await unitOfWork.Planes.GetByAsync(plane_id);
            var maker = await unitOfWork.Makers.GetByAsync(plane.MakerId);
            return String.Format("{0}-{1}", maker.MakerName, plane.PlaneId);
        }
        public async Task<String> getPlaneFullname(Plane plane, string maker_id)
        {
            var maker = await unitOfWork.Makers.GetByAsync(plane.MakerId);
            return String.Format("{0}-{1}", maker.MakerName, plane.PlaneId);
        }

        public async Task<IEnumerable<PlaneDTO>> getAvailablePlaneAsync()
        {
            var Planes = await unitOfWork.Planes.getAvailablePlane();
            return this.toDtoRange(Planes);
        }
        public async Task<IEnumerable<PlaneDTO>> getDisabledPlaneAsync()
        {
            var Planes = await unitOfWork.Planes.getDisabledPlane();
            return this.toDtoRange(Planes);
        }

        new public async Task<IEnumerable<Plane>> SortAsync(IEnumerable<Plane> entities, ORDER_ENUM col, ORDER_ENUM order)
        {
            IEnumerable<Plane> res = null;
            await Task.Run(() => true);
            if (order == ORDER_ENUM.DESCENDING)
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: case ORDER_ENUM.PLANE_NAME: res = entities.OrderByDescending(m => this.getPlaneFullname(m, m.MakerId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.MAKER_NAME: res = entities.OrderByDescending(m => unitOfWork.Makers.GetByAsync(m.MakerId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderByDescending(m => m.Status); break;
                    default: res = entities.OrderByDescending(m => m.PlaneId); break;
                }
            }
            else
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: case ORDER_ENUM.PLANE_NAME: res = entities.OrderBy(m => this.getPlaneFullname(m, m.MakerId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.MAKER_NAME: res = entities.OrderBy(m => unitOfWork.Makers.GetByAsync(m.MakerId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderBy(m => m.Status); break;
                    default: res = entities.OrderBy(m => m.PlaneId); break;
                }

            }
            return res;
        }



        //actions
        private async Task generatePlaneId(Plane Plane)
        {
            var res = await unitOfWork.Planes.GetAllAsync();
            res = res.OrderBy(m => m.PlaneId);
            string id = null;
            if (res != null) id = res.LastOrDefault().PlaneId;
            var code = 0;
            Int32.TryParse(id, out code);
            Plane.PlaneId = String.Format("{0:00000}", code + 1);
        }
        public async Task addPlaneAsync(PlaneDTO dto)
        {
            if (await unitOfWork.Planes.GetByAsync(dto.PlaneId) == null)
            {
                var plane = this.toEntity(dto);
                await this.generatePlaneId(plane);
                await unitOfWork.Planes.AddAsync(plane);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task removePlaneAsync(string plane_id)
        {
            var plane = await unitOfWork.Planes.GetByAsync(plane_id);
            if (plane != null)
            {
                await unitOfWork.Planes.RemoveAsync(plane);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task updatePlaneAsync(PlaneDTO dto)
        {
            if (await unitOfWork.Planes.GetByAsync(dto.PlaneId) != null)
            {
                // var Plane = this.toEntity(dto);
                // await unitOfWork.Planes.UpdateAsync(Plane);
                var Plane = await unitOfWork.Planes.GetByAsync(dto.PlaneId);
                this.convertDtoToEntity(dto, Plane);
            }
            else
            {
                var plane = this.toEntity(dto);
                await this.generatePlaneId(plane);
                await unitOfWork.Planes.AddAsync(plane);
            }
            await unitOfWork.CompleteAsync();
        }

        public async Task disablePlaneAsync(string cus_id)
        {
            var cus = await unitOfWork.Planes.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Planes.disable(cus);
        }
        public async Task activatePlaneAsync(string cus_id)
        {
            var cus = await unitOfWork.Planes.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Planes.activate(cus);
        }
    }
}
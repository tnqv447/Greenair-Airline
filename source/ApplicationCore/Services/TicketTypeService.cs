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
    public class TicketTypeService : Service<TicketType, TicketTypeDTO, TicketTypeDTO>, ITicketTypeService
    {
        // public IEnumerable<TicketType> List { get; set; }
        public TicketTypeService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            // List = unitOfWork.TicketTypes.GetAllAsync().GetAwaiter().GetResult();
        }
        //query
        public async Task<TicketTypeDTO> getTicketTypeAsync(string ticket_type_id)
        {
            return this.toDto(await unitOfWork.TicketTypes.GetByAsync(ticket_type_id));
        }
        public async Task<IEnumerable<TicketTypeDTO>> getAllTicketTypeAsync()
        {
            return this.toDtoRange(await unitOfWork.TicketTypes.GetAllAsync());
        }

        public async Task<IEnumerable<TicketTypeDTO>> getAvailableTicketTypeAsync()
        {
            var TicketTypes = await unitOfWork.TicketTypes.getAvailableTicketType();
            return this.toDtoRange(TicketTypes);
        }
        public async Task<IEnumerable<TicketTypeDTO>> getDisabledTicketTypeAsync()
        {
            var TicketTypes = await unitOfWork.TicketTypes.getDisabledTicketType();
            return this.toDtoRange(TicketTypes);
        }

        new public async Task<IEnumerable<TicketType>> SortAsync(IEnumerable<TicketType> entities, ORDER_ENUM col, ORDER_ENUM order)
        {
            IEnumerable<TicketType> res = null;
            await Task.Run(() => true);
            if (order == ORDER_ENUM.DESCENDING)
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderByDescending(m => m.TicketTypeName); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderByDescending(m => m.Status); break;
                    case ORDER_ENUM.BASEPRICE: res = entities.OrderByDescending(m => m.BasePrice); break;
                    default: res = entities.OrderByDescending(m => m.TicketTypeId); break;
                }
            }
            else
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderBy(m => m.TicketTypeName); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderBy(m => m.Status); break;
                    case ORDER_ENUM.BASEPRICE: res = entities.OrderBy(m => m.BasePrice); break;
                    default: res = entities.OrderBy(m => m.TicketTypeId); break;
                }

            }
            return res;
        }


        //actions
        private async Task generateTicketTypeId(TicketType TicketType)
        {
            var res = await unitOfWork.TicketTypes.GetAllAsync();
            res = res.OrderBy(m => m.TicketTypeId);
            string id = null;
            if (res != null) id = res.LastOrDefault().TicketTypeId;
            var code = 0;
            Int32.TryParse(id, out code);
            TicketType.TicketTypeId = String.Format("{0:000}", code + 1);
        }
        public async Task addTicketTypeAsync(TicketTypeDTO dto)
        {
            if (await unitOfWork.TicketTypes.GetByAsync(dto.TicketTypeId) == null)
            {
                var ticket_type = this.toEntity(dto);
                await this.generateTicketTypeId(ticket_type);
                await unitOfWork.TicketTypes.AddAsync(ticket_type);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task removeTicketTypeAsync(string ticket_type_id)
        {
            var ticket_type = await unitOfWork.TicketTypes.GetByAsync(ticket_type_id);
            if (ticket_type != null)
            {
                await unitOfWork.TicketTypes.RemoveAsync(ticket_type);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task updateTicketTypeAsync(TicketTypeDTO dto)
        {
            if (await unitOfWork.TicketTypes.GetByAsync(dto.TicketTypeId) != null)
            {
                // var TicketType = this.toEntity(dto);
                // await unitOfWork.TicketTypes.UpdateAsync(TicketType);
                var TicketType = await unitOfWork.TicketTypes.GetByAsync(dto.TicketTypeId);
                this.convertDtoToEntity(dto, TicketType);
            }
            else
            {
                var ticket_type = this.toEntity(dto);
                await this.generateTicketTypeId(ticket_type);
                await unitOfWork.TicketTypes.AddAsync(ticket_type);
            }
            await unitOfWork.CompleteAsync();
        }

        public async Task disableTicketTypeAsync(string cus_id)
        {
            var cus = await unitOfWork.TicketTypes.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.TicketTypes.disable(cus);
        }
        public async Task activateTicketTypeAsync(string cus_id)
        {
            var cus = await unitOfWork.TicketTypes.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.TicketTypes.activate(cus);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using ApplicationCore;
namespace Infrastructure.Persistence.Repos
{
    public class TicketTypeRepository : Repository<TicketType>, ITicketTypeRepository
    {
        protected new GreenairContext Context => base.Context as GreenairContext;
        public TicketTypeRepository(GreenairContext context) : base(context)
        {
        }

        private async Task<IEnumerable<TicketType>> getTicketTypesByStatus(STATUS status)
        {
            return await this.FindAsync(m => m.Status == status);
        }

        public async Task<IEnumerable<TicketType>> getAvailableTicketType()
        {
            return await this.getTicketTypesByStatus(STATUS.AVAILABLE);
        }

        public async Task<IEnumerable<TicketType>> getDisabledTicketType()
        {
            return await this.getTicketTypesByStatus(STATUS.DISABLED);
        }

        private async Task changeTicketTypeStatus(string TicketType_id, STATUS status)
        {
            try
            {
                var TicketType = await this.GetByAsync(TicketType_id);
                TicketType.Status = status;
                this.Context.Update(TicketType);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeTicketTypeStatus() Unexpected: " + e);
            }
        }

        private async Task changeTicketTypeStatus(TicketType TicketType, STATUS status)
        {
            try
            {
                TicketType.Status = status;
                this.Context.Update(TicketType);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeTicketTypeStatus() Unexpected: " + e);
            }
        }

        public async Task activate(string TicketType_id)
        {
            await this.changeTicketTypeStatus(TicketType_id, STATUS.AVAILABLE);
        }

        public async Task disable(string TicketType_id)
        {
            await this.changeTicketTypeStatus(TicketType_id, STATUS.DISABLED);
        }

        public async Task activate(TicketType TicketType)
        {
            await this.changeTicketTypeStatus(TicketType, STATUS.AVAILABLE);
        }

        public async Task disable(TicketType TicketType)
        {
            await this.changeTicketTypeStatus(TicketType, STATUS.DISABLED);
        }
    }
}
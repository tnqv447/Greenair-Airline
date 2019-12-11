using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using ApplicationCore;
using System;
using LinqKit;
namespace Infrastructure.Persistence.Repos
{
    public class MakerRepository : Repository<Maker>, IMakerRepository
    {
        protected new GreenairContext Context => base.Context as GreenairContext;
        public MakerRepository(GreenairContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Maker>> getMakerByName(string name)
        {
            var res = await this.GetAllAsync();
            res = res.Where(m => m.MakerName.Contains(name, StringComparison.OrdinalIgnoreCase));
            return res;
        }

        private async Task<IEnumerable<Maker>> getMakersByStatus(STATUS status)
        {
            return await this.FindAsync(m => m.Status == status);
        }

        public async Task<IEnumerable<Maker>> getAvailableMaker()
        {
            return await this.getMakersByStatus(STATUS.AVAILABLE);
        }

        public async Task<IEnumerable<Maker>> getDisabledMaker()
        {
            return await this.getMakersByStatus(STATUS.DISABLED);
        }

        private async Task changeMakerStatus(string Maker_id, STATUS status)
        {
            try
            {
                var Maker = await this.GetByAsync(Maker_id);
                Maker.Status = status;
                this.Context.Update(Maker);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeMakerStatus() Unexpected: " + e);
            }
        }

        private async Task changeMakerStatus(Maker Maker, STATUS status)
        {
            try
            {
                Maker.Status = status;
                this.Context.Update(Maker);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeMakerStatus() Unexpected: " + e);
            }
        }

        public async Task activate(string Maker_id)
        {
            await this.changeMakerStatus(Maker_id, STATUS.AVAILABLE);
        }

        public async Task disable(string Maker_id)
        {
            await this.changeMakerStatus(Maker_id, STATUS.DISABLED);
        }

        public async Task activate(Maker Maker)
        {
            await this.changeMakerStatus(Maker, STATUS.AVAILABLE);
        }

        public async Task disable(Maker Maker)
        {
            await this.changeMakerStatus(Maker, STATUS.DISABLED);
        }
    }
}
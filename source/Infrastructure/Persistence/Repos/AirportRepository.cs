using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using ApplicationCore;
using System;
using System.Linq;
using LinqKit;
namespace Infrastructure.Persistence.Repos
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        protected new GreenairContext Context => base.Context as GreenairContext;
        public AirportRepository(GreenairContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Airport>> getAirportByConditions(string airport_name, string city, string country)
        {
            var res = await this.GetAllAsync();
            if (!String.IsNullOrEmpty(airport_name)) res.Where(m => m.AirportName.Contains(airport_name, StringComparison.OrdinalIgnoreCase));
            if (!String.IsNullOrEmpty(city)) res.Where(m => m.Address.City.Contains(city, StringComparison.OrdinalIgnoreCase));
            if (!String.IsNullOrEmpty(country)) res.Where(m => m.Address.Country.Contains(country, StringComparison.OrdinalIgnoreCase));
            return res;

        }
        public async Task<bool> isDomestic(string airport_id)
        {
            var airport = await this.GetByAsync(airport_id);
            return airport.Address.isDomestic();
        }

        private async Task<IEnumerable<Airport>> getAirportsByStatus(STATUS status)
        {
            return await this.FindAsync(m => m.Status == status);
        }

        public async Task<IEnumerable<Airport>> getAvailableAirport()
        {
            return await this.getAirportsByStatus(STATUS.AVAILABLE);
        }

        public async Task<IEnumerable<Airport>> getDisabledAirport()
        {
            return await this.getAirportsByStatus(STATUS.DISABLED);
        }

        private async Task changeAirportStatus(string Airport_id, STATUS status)
        {
            try
            {
                var Airport = await this.GetByAsync(Airport_id);
                Airport.Status = status;
                this.Context.Update(Airport);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeAirportStatus() Unexpected: " + e);
            }
        }

        private async Task changeAirportStatus(Airport Airport, STATUS status)
        {
            try
            {
                Airport.Status = status;
                this.Context.Update(Airport);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeAirportStatus() Unexpected: " + e);
            }
        }

        public async Task activate(string Airport_id)
        {
            await this.changeAirportStatus(Airport_id, STATUS.AVAILABLE);
        }

        public async Task disable(string Airport_id)
        {
            await this.changeAirportStatus(Airport_id, STATUS.DISABLED);
        }

        public async Task activate(Airport Airport)
        {
            await this.changeAirportStatus(Airport, STATUS.AVAILABLE);
        }

        public async Task disable(Airport Airport)
        {
            await this.changeAirportStatus(Airport, STATUS.DISABLED);
        }
    }
}
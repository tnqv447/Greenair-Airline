using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using System;

namespace Infrastructure.Persistence.Repos
{
    public class PlaneRepository : Repository<Plane>, IPlaneRepository
    {
        protected new GreenairContext Context => base.Context as GreenairContext;
        public PlaneRepository(GreenairContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Plane>> getPlanebyMakerId(string maker_id)
        {
            var predicate = PredicateBuilder.New<Plane>();
            predicate = predicate.And(m => m.MakerId.Equals(maker_id));
            return await this.FindAsync(predicate);
        }

        private async Task<IEnumerable<Plane>> getPlanesByStatus(STATUS status)
        {
            return await this.FindAsync(m => m.Status == status);
        }

        public async Task<IEnumerable<Plane>> getAvailablePlane()
        {
            return await this.getPlanesByStatus(STATUS.AVAILABLE);
        }

        public async Task<IEnumerable<Plane>> getDisabledPlane()
        {
            return await this.getPlanesByStatus(STATUS.DISABLED);
        }

        private async Task changePlaneStatus(string Plane_id, STATUS status)
        {
            try
            {
                var Plane = await this.GetByAsync(Plane_id);
                Plane.Status = status;
                this.Context.Update(Plane);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangePlaneStatus() Unexpected: " + e);
            }
        }

        private async Task changePlaneStatus(Plane Plane, STATUS status)
        {
            try
            {
                Plane.Status = status;
                this.Context.Update(Plane);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangePlaneStatus() Unexpected: " + e);
            }
        }

        public async Task activate(string Plane_id)
        {
            await this.changePlaneStatus(Plane_id, STATUS.AVAILABLE);
        }

        public async Task disable(string Plane_id)
        {
            await this.changePlaneStatus(Plane_id, STATUS.DISABLED);
        }

        public async Task activate(Plane Plane)
        {
            await this.changePlaneStatus(Plane, STATUS.AVAILABLE);
        }

        public async Task disable(Plane Plane)
        {
            await this.changePlaneStatus(Plane, STATUS.DISABLED);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using ApplicationCore;
using System.Collections.Generic;
using LinqKit;
namespace Infrastructure.Persistence.Repos
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        protected new GreenairContext Context => base.Context as GreenairContext;
        public JobRepository(GreenairContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Job>> getJobByName(string job_name)
        {
            var res = await this.GetAllAsync();
            res = res.Where(m => m.JobName.Contains(job_name, StringComparison.OrdinalIgnoreCase));
            return res;
        }

        private async Task<IEnumerable<Job>> getJobsByStatus(STATUS status)
        {
            return await this.FindAsync(m => m.Status == status);
        }

        public async Task<IEnumerable<Job>> getAvailableJob()
        {
            return await this.getJobsByStatus(STATUS.AVAILABLE);
        }

        public async Task<IEnumerable<Job>> getDisabledJob()
        {
            return await this.getJobsByStatus(STATUS.DISABLED);
        }

        private async Task changeJobStatus(string Job_id, STATUS status)
        {
            try
            {
                var Job = await this.GetByAsync(Job_id);
                Job.Status = status;
                this.Context.Update(Job);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeJobStatus() Unexpected: " + e);
            }
        }

        private async Task changeJobStatus(Job Job, STATUS status)
        {
            try
            {
                Job.Status = status;
                this.Context.Update(Job);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeJobStatus() Unexpected: " + e);
            }
        }

        public async Task activate(string Job_id)
        {
            await this.changeJobStatus(Job_id, STATUS.AVAILABLE);
        }

        public async Task disable(string Job_id)
        {
            await this.changeJobStatus(Job_id, STATUS.DISABLED);
        }

        public async Task activate(Job Job)
        {
            await this.changeJobStatus(Job, STATUS.AVAILABLE);
        }

        public async Task disable(Job Job)
        {
            await this.changeJobStatus(Job, STATUS.DISABLED);
        }


    }
}
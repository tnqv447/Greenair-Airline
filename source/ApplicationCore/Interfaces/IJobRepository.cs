using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore;
namespace ApplicationCore.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<IEnumerable<Job>> getJobByName(string job_name);

        Task<IEnumerable<Job>> getAvailableJob();
        Task<IEnumerable<Job>> getDisabledJob();

        Task disable(string job_id);
        Task activate(string job_id);
        Task disable(Job acc);
        Task activate(Job acc);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
namespace ApplicationCore.Services
{
    public interface IJobService : IService<Job, JobDTO, JobDTO>
    {
        //query
        Task<JobDTO> getJobAsync(string job_id);
        Task<IEnumerable<JobDTO>> getAllJobAsync();
        Task<IEnumerable<JobDTO>> getJobByNameAsync(string job_name);

        Task<IEnumerable<JobDTO>> getAvailableJobAsync();
        Task<IEnumerable<JobDTO>> getDisabledJobAsync();

        //actions
        Task addJobAsync(JobDTO dto);
        Task removeJobAsync(string job_id);
        Task updateJobAsync(JobDTO dto);

        Task disableJobAsync(string Job_id);
        Task activateJobAsync(string Job_id);
    }
}
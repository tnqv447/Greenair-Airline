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
    public class JobService : Service<Job, JobDTO, JobDTO>, IJobService
    {
        // public IEnumerable<Job> List { get; set; }
        public JobService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            // List = unitOfWork.Jobs.GetAllAsync().GetAwaiter().GetResult();
        }
        //query
        public async Task<JobDTO> getJobAsync(string job_id)
        {
            return this.toDto(await unitOfWork.Jobs.GetByAsync(job_id));
        }
        public async Task<IEnumerable<JobDTO>> getAllJobAsync()
        {
            return this.toDtoRange(await unitOfWork.Jobs.GetAllAsync());
        }
        public async Task<IEnumerable<JobDTO>> getJobByNameAsync(string job_name)
        {
            return this.toDtoRange(await unitOfWork.Jobs.getJobByName(job_name));
        }

        public async Task<IEnumerable<JobDTO>> getAvailableJobAsync()
        {
            var Jobs = await unitOfWork.Jobs.getAvailableJob();
            return this.toDtoRange(Jobs);
        }
        public async Task<IEnumerable<JobDTO>> getDisabledJobAsync()
        {
            var Jobs = await unitOfWork.Jobs.getDisabledJob();
            return this.toDtoRange(Jobs);
        }

        new public async Task<IEnumerable<JobDTO>> SortAsync(IEnumerable<JobDTO> entities, ORDER_ENUM col, ORDER_ENUM order)
        {
            IEnumerable<JobDTO> res = null;
            await Task.Run(() => true);
            if (order == ORDER_ENUM.DESCENDING)
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderByDescending(m => m.JobName); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderByDescending(m => m.Status); break;
                    default: res = entities.OrderByDescending(m => m.JobId); break;
                }
            }
            else
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderBy(m => m.JobName); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderBy(m => m.Status); break;
                    default: res = entities.OrderBy(m => m.JobId); break;
                }

            }
            return res;
        }

        //actions
        private async Task generateJobId(Job Job)
        {
            var res = await unitOfWork.Jobs.GetAllAsync();
            res = res.OrderBy(m => m.JobId);
            string id = null;
            if (res != null) id = res.LastOrDefault().JobId;
            var code = 0;
            Int32.TryParse(id, out code);
            Job.JobId = String.Format("{0:000}", code + 1);
        }
        public async Task addJobAsync(JobDTO dto)
        {
            if (await unitOfWork.Jobs.GetByAsync(dto.JobId) == null)
            {
                var job = this.toEntity(dto);
                await this.generateJobId(job);
                await unitOfWork.Jobs.AddAsync(job);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task removeJobAsync(string job_id)
        {
            var job = await unitOfWork.Jobs.GetByAsync(job_id);
            if (job != null)
            {
                await unitOfWork.Jobs.RemoveAsync(job);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task updateJobAsync(JobDTO dto)
        {
            if (await unitOfWork.Jobs.GetByAsync(dto.JobId) != null)
            {
                // var Job = this.toEntity(dto);
                // await unitOfWork.Jobs.UpdateAsync(Job);
                var Job = await unitOfWork.Jobs.GetByAsync(dto.JobId);
                this.convertDtoToEntity(dto, Job);

            }
            else
            {
                var job = this.toEntity(dto);
                await this.generateJobId(job);
                await unitOfWork.Jobs.AddAsync(job);
            }
            await unitOfWork.CompleteAsync();
        }

        public async Task disableJobAsync(string cus_id)
        {
            var cus = await unitOfWork.Jobs.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Jobs.disable(cus);
        }
        public async Task activateJobAsync(string cus_id)
        {
            var cus = await unitOfWork.Jobs.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Jobs.activate(cus);
        }
    }
}
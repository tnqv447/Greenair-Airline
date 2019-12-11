using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Interfaces;

namespace ApplicationCore.DTOs
{
    public class JobDTO : IAggregateRoot
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string JobId { get; set; }

        [StringLength(20, MinimumLength = 5)]
        public string JobName { get; set; }

        public STATUS Status { get; set; }

        //public IList<EmployeeDTO> Employees { get; set; }

        public JobDTO(string jobId, string jobName)
        {
            this.JobId = jobId;
            this.JobName = jobName;

        }

    }
}
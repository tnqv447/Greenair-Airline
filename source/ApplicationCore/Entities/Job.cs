using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Interfaces;
namespace ApplicationCore.Entities
{
    public class Job : IAggregateRoot
    {
        public string JobId { get; set; }

        public string JobName { get; set; }
        public IList<Employee> Employees { get; set; }

        public STATUS Status { get; set; }

        public Job(string jobId, string jobName)
        {
            this.JobId = jobId;
            this.JobName = jobName;

        }

    }
}
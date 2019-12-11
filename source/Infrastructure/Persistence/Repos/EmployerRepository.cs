using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using ApplicationCore;
using LinqKit;
namespace Infrastructure.Persistence.Repos
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        protected new GreenairContext Context => base.Context as GreenairContext;
        public EmployeeRepository(GreenairContext context) : base(context)
        {
        }

        new public async Task<Employee> GetByAsync(string id)
        {
            try
            {
                var predicate = PredicateBuilder.New<Employee>();
                predicate = predicate.And(m => m.Id.Equals(id));
                var res = await Task.Run(() => this.Context.Employees.AsNoTracking().Where(predicate).ToList());
                if (res.Count() != 1) return null;
                else return res.ElementAt(0);

            }
            catch (Exception e)
            {
                Console.WriteLine("GetByEmployeeAsync() Unexpected: " + e);
                return null;
            }
        }
        public async Task<IEnumerable<Employee>> getEmployeeByName(string lastname, string firstname)
        {
            var res = await this.GetAllAsync();
            if (!String.IsNullOrEmpty(lastname)) res = res.Where(m => m.LastName.Contains(lastname, StringComparison.OrdinalIgnoreCase));
            if (!String.IsNullOrEmpty(firstname)) res = res.Where(m => m.FirstName.Contains(firstname, StringComparison.OrdinalIgnoreCase));
            return res;
        }
        public async Task<IEnumerable<Employee>> getEmployeeByName(string fullname)
        {
            var res = await this.GetAllAsync();
            if (!String.IsNullOrEmpty(fullname)) res = res.Where(m => m.FullName.Contains(fullname, StringComparison.OrdinalIgnoreCase));
            return res;
        }
        public async Task<IEnumerable<Employee>> getEmployeeByJob(string job_id)
        {
            var predicate = PredicateBuilder.New<Employee>(m => m.JobId.Equals(job_id));
            return await this.FindAsync(predicate);
        }


        private async Task<IEnumerable<Employee>> getEmployeesByStatus(STATUS status)
        {
            return await this.FindAsync(m => m.Status == status);
        }

        public async Task<IEnumerable<Employee>> getAvailableEmployee()
        {
            return await this.getEmployeesByStatus(STATUS.AVAILABLE);
        }

        public async Task<IEnumerable<Employee>> getDisabledEmployee()
        {
            return await this.getEmployeesByStatus(STATUS.DISABLED);
        }

        private async Task changeEmployeeStatus(string emp_id, STATUS status)
        {
            try
            {
                var emp = await this.GetByAsync(emp_id);
                emp.Status = status;
                this.Context.Update(emp);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeEmployeeStatus() Unexpected: " + e);
            }
        }
        private async Task changeEmployeeStatus(Employee emp, STATUS status)
        {
            try
            {
                emp.Status = status;
                this.Context.Update(emp);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeEmployeeStatus() Unexpected: " + e);
            }
        }

        public async Task activate(string emp_id)
        {
            await this.changeEmployeeStatus(emp_id, STATUS.AVAILABLE);
        }

        public async Task disable(string emp_id)
        {
            await this.changeEmployeeStatus(emp_id, STATUS.DISABLED);
        }

        public async Task activate(Employee emp)
        {
            await this.changeEmployeeStatus(emp, STATUS.AVAILABLE);
        }

        public async Task disable(Employee emp)
        {
            await this.changeEmployeeStatus(emp, STATUS.DISABLED);
        }
    }
}
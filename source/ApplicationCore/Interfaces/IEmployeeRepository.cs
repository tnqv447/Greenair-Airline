using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
namespace ApplicationCore.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> getEmployeeByName(string lastname, string firstname);
        Task<IEnumerable<Employee>> getEmployeeByName(string fullname);
        Task<IEnumerable<Employee>> getEmployeeByJob(string job_id);

        Task<IEnumerable<Employee>> getAvailableEmployee();
        Task<IEnumerable<Employee>> getDisabledEmployee();
        
        Task disable(string emp_id);
        Task activate(string emp_id);
        Task disable(Employee emp);
        Task activate(Employee emp);
    }
}
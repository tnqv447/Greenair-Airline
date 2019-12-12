using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
namespace ApplicationCore.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> getCustomerByName(string lastname, string firstname);
        Task<IEnumerable<Customer>> getCustomerByName(string fullname);

        Task<IEnumerable<Customer>> getAvailableCustomer();
        Task<IEnumerable<Customer>> getDisabledCustomer();

        Task disable(string cus_id);
        Task activate(string cus_id);
        Task disable(Customer cus);
        Task activate(Customer cus);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.DTOs;
namespace ApplicationCore.Services
{
    public interface ICustomerService : IService<Customer, CustomerDTO, CustomerDTO>
    {
        //query
        Task<CustomerDTO> getCustomerAsync(string cus_id);
        Task<IEnumerable<CustomerDTO>> getAllCustomerAsync();
        Task<IEnumerable<TicketDTO>> getCustomerTicketAsync(string cus_id);
        Task<IEnumerable<CustomerDTO>> getCustomerByName(string lastname, string firstname);
        Task<IEnumerable<CustomerDTO>> getCustomerByName(string fullname);

        Task<IEnumerable<CustomerDTO>> getAvailableCustomerAsync();
        Task<IEnumerable<CustomerDTO>> getDisabledCustomerAsync();

        //actions
        Task orderTicketAsync(string flight_id, string cus_id, string assined_cus, string ticket_type_id);
        Task orderTicketRangeAsync(string flight_id, int adult_num, int child_num, string cus_id);
        Task payTicketAsync(string flight_id, string ticket_id);
        Task cancelTicketAsync(string flight_id, string ticket_id);

        Task addCustomerAsync(CustomerDTO dto);
        Task removeCustomerAsync(string cus_id);
        Task updateCustomerAsync(CustomerDTO dto);

        Task disableCustomerAsync(string cus_id);
        Task activateCustomerAsync(string cus_id);
    }
}
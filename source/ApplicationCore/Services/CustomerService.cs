using System.Linq;
using ApplicationCore.Entities;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System;
using LinqKit;
namespace ApplicationCore.Services
{
    public class CustomerService : Service<Customer, CustomerDTO, CustomerDTO>, ICustomerService
    {

        // public IEnumerable<Customer> List { get; set; }
        public CustomerService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            // List = unitOfWork.Customers.GetAllAsync().GetAwaiter().GetResult();
        }
        //query
        public async Task<CustomerDTO> getCustomerAsync(string cus_id)
        {
            var cus = await unitOfWork.Customers.GetByAsync(cus_id);
            Console.WriteLine("qwerty {0}", cus.FullName);
            if (cus == null) return null;
            return toDto(cus);
        }
        public async Task<IEnumerable<CustomerDTO>> getAllCustomerAsync()
        {
            return toDtoRange(await unitOfWork.Customers.GetAllAsync());
        }
        public async Task<IEnumerable<TicketDTO>> getCustomerTicketAsync(string cus_id)
        {
            var predicate = PredicateBuilder.New<Ticket>();
            predicate = predicate.And(m => m.CustomerId.Equals(cus_id));
            var tickets = await unitOfWork.Flights.findTicketAsync(predicate);
            return mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDTO>>(tickets);
        }

        public async Task<IEnumerable<CustomerDTO>> getCustomerByName(string lastname, string firstname)
        {
            return this.toDtoRange(await unitOfWork.Customers.getCustomerByName(lastname, firstname));
        }
        public async Task<IEnumerable<CustomerDTO>> getCustomerByName(string fullname)
        {
            return this.toDtoRange(await unitOfWork.Customers.getCustomerByName(fullname));
        }

        public async Task<IEnumerable<CustomerDTO>> getAvailableCustomerAsync()
        {
            var Customers = await unitOfWork.Customers.getAvailableCustomer();
            return this.toDtoRange(Customers);
        }
        public async Task<IEnumerable<CustomerDTO>> getDisabledCustomerAsync()
        {
            var Customers = await unitOfWork.Customers.getDisabledCustomer();
            return this.toDtoRange(Customers);
        }

        new public async Task<IEnumerable<CustomerDTO>> SortAsync(IEnumerable<CustomerDTO> entities, ORDER_ENUM col, ORDER_ENUM order)
        {
            IEnumerable<CustomerDTO> res = null;
            await Task.Run(() => true);
            if (order == ORDER_ENUM.DESCENDING)
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderByDescending(m => m.FullName); break;
                    case ORDER_ENUM.FIRST_NAME: res = entities.OrderByDescending(m => m.FirstName); break;
                    case ORDER_ENUM.LAST_NAME: res = entities.OrderByDescending(m => m.LastName); break;
                    case ORDER_ENUM.ADDRESS: res = entities.OrderByDescending(m => m.Address.ToString()); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderByDescending(m => m.Status); break;
                    default: res = entities.OrderByDescending(m => m.Id); break;
                }
            }
            else
            {
                switch (col)
                {
                    case ORDER_ENUM.NAME: res = entities.OrderBy(m => m.FullName); break;
                    case ORDER_ENUM.FIRST_NAME: res = entities.OrderBy(m => m.FirstName); break;
                    case ORDER_ENUM.LAST_NAME: res = entities.OrderBy(m => m.LastName); break;
                    case ORDER_ENUM.ADDRESS: res = entities.OrderBy(m => m.Address.ToString()); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderBy(m => m.Status); break;
                    default: res = entities.OrderBy(m => m.Id); break;
                }

            }
            return res;
        }







        //actions
        public async Task orderTicketAsync(string flight_id, string cus_id, string assined_cus, string ticket_type_id)
        {
            var tickets = await unitOfWork.Flights.getAvailableTickets(flight_id);
            if (tickets.Count() >= 1)
            {
                var ticket = tickets.ElementAt(0);
                await unitOfWork.Flights.orderTicket(ticket, cus_id, assined_cus, ticket_type_id);
            }
        }
        public async Task orderTicketRangeAsync(string flight_id, int adult_num, int child_num, string cus_id)
        {
            int sum = adult_num + child_num;
            var tickets = await unitOfWork.Flights.getAvailableTickets(flight_id);
            if (tickets.Count() >= sum)
            {
                for (int i = 0; i < sum; i++)
                {
                    if (i < adult_num)
                    {
                        await unitOfWork.Flights.orderTicket(tickets.ElementAt(i), cus_id, null, "000");
                    }
                    else
                    {
                        await unitOfWork.Flights.orderTicket(tickets.ElementAt(i), cus_id, null, "001");
                    }
                }
            }
        }
        public async Task payTicketAsync(string flight_id, string ticket_id)
        {
            var ticket = await unitOfWork.Flights.getTicket(flight_id, ticket_id);
            if (ticket != null && ticket.Status == STATUS.ORDERED)
            {
                await unitOfWork.Flights.paidTicket(ticket);
            }
        }
        public async Task cancelTicketAsync(string flight_id, string ticket_id)
        {
            var ticket = await unitOfWork.Flights.getTicket(flight_id, ticket_id);
            if (ticket != null && ticket.Status == STATUS.ORDERED)
            {
                await unitOfWork.Flights.cancelTicket(ticket);
            }
        }

        private async Task generateCustomerId(Customer Customer)
        {
            var res = await unitOfWork.Persons.GetAllAsync();
            res = res.OrderBy(m => m.Id);
            string id = null;
            if (res != null) id = res.LastOrDefault().Id;
            var code = 0;
            Int32.TryParse(id, out code);
            Customer.Id = String.Format("{0:00000}", code + 1);
        }
        public async Task addCustomerAsync(CustomerDTO dto)
        {
            if (await unitOfWork.Customers.GetByAsync(dto.Id) == null)
            {
                var cus = this.toEntity(dto);
                await this.generateCustomerId(cus);
                await unitOfWork.Customers.AddAsync(cus);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task removeCustomerAsync(string cus_id)
        {
            var cus = await unitOfWork.Customers.GetByAsync(cus_id);
            if (cus != null)
            {
                await unitOfWork.Customers.RemoveAsync(cus);
                await unitOfWork.CompleteAsync();
            }
        }
        public async Task updateCustomerAsync(CustomerDTO dto)
        {
            if (await unitOfWork.Customers.GetByAsync(dto.Id) != null)
            {
                var cus = this.toEntity(dto);
                await unitOfWork.Customers.UpdateAsync(cus);
            }
            else
            {
                var cus = this.toEntity(dto);
                await this.generateCustomerId(cus);
                await unitOfWork.Customers.AddAsync(cus);
            }
            await unitOfWork.CompleteAsync();
        }


        public async Task disableCustomerAsync(string cus_id)
        {
            var cus = await unitOfWork.Customers.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Customers.disable(cus);
        }
        public async Task activateCustomerAsync(string cus_id)
        {
            var cus = await unitOfWork.Customers.GetByAsync(cus_id);
            if (cus != null) await unitOfWork.Customers.activate(cus);
        }
    }
}
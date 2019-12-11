using System.Threading.Tasks;
using ApplicationCore.Entities;
using System.Collections.Generic;
namespace ApplicationCore.Interfaces
{
    public interface ITicketTypeRepository : IRepository<TicketType>
    {

        Task<IEnumerable<TicketType>> getAvailableTicketType();
        Task<IEnumerable<TicketType>> getDisabledTicketType();
        
        Task disable(string TicketType_id);
        Task activate(string TicketType_id);
        Task disable(TicketType acc);
        Task activate(TicketType acc);
    }
}
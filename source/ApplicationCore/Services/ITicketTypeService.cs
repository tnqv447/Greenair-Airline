using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
namespace ApplicationCore.Services
{
    public interface ITicketTypeService : IService<TicketType, TicketTypeDTO, TicketTypeDTO>
    {
        //query
        Task<TicketTypeDTO> getTicketTypeAsync(string ticketType_id);
        Task<IEnumerable<TicketTypeDTO>> getAllTicketTypeAsync();

        Task<IEnumerable<TicketTypeDTO>> getAvailableTicketTypeAsync();
        Task<IEnumerable<TicketTypeDTO>> getDisabledTicketTypeAsync();

        //actions
        Task addTicketTypeAsync(TicketTypeDTO dto);
        Task removeTicketTypeAsync(string ticketType_id);
        Task updateTicketTypeAsync(TicketTypeDTO dto);

        Task disableTicketTypeAsync(string TicketType_id);
        Task activateTicketTypeAsync(string TicketType_id);
    }
}
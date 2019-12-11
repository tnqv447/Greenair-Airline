using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ApplicationCore.Entities;
using ApplicationCore;
namespace ApplicationCore.Interfaces
{
    public interface IFlightRepository : IRepository<Flight>
    {
        //Tra ket qua ================================================================================
        Task<string> getFirstRouteId(string flight_id);
        Task<string> getLastRouteId(string flight_id);
        Task<DateTime> getArrDate(string flight_id);
        Task<DateTime> getDepDate(string flight_id);

        //Thao tac voi chuyen bay ==================================================================================
        // private Task changeFlightStatus(string flight_id, STATUS status);
        Task disable(string flight_id);
        Task activate(string flight_id);
        Task<IEnumerable<Flight>> getAvailableFlights();
        Task<IEnumerable<Flight>> getDisabledFlights();

        //Thao tac voi chi tiet chuyen bay ===================================================================================
        Task<FlightDetail> getFlightDetail(string flight_id, int index);
        Task<FlightDetail> getFlightDetail(string flight_id, string flightdetail_id);
        Task<IEnumerable<FlightDetail>> getAllFlightDetails(string flight_id);
        Task addFlightDetail(FlightDetail det);
        Task addFlightDetailRange(IEnumerable<FlightDetail> dets);
        Task removeFlightDetail(string flight_id); //xoa detail dung cuoi
        Task removeAllFlightDetail(string flight_id);

        //Thao tac voi ve ====================================================================
        Task<Ticket> getTicket(string flight_id, string ticket_id);
        Task<IEnumerable<Ticket>> getAllTickets();
        Task<IEnumerable<Ticket>> getAllTicketsByFlightId(string flight_id);
        //Task<IEnumerable<Ticket>> getTicketsByStatus(string flight_id, STATUS status);
        Task<IEnumerable<Ticket>> getAvailableTickets(string flight_id);
        Task<IEnumerable<Ticket>> getOrderedTickets(string flight_id);
        Task<IEnumerable<Ticket>> getPaidTickets(string flight_id);
        Task<IEnumerable<Ticket>> findTicketAsync(Expression<Func<Ticket, bool>> predicate);
        Task addTicket(Ticket ticket);
        Task addTicketRange(IEnumerable<Ticket> tickets);

        // private Task changeTicketStatus(string flight_id, string ticket_id, STATUS status);
        Task paidTicket(string flight_id, string ticket_id);
        Task orderTicket(string flight_id, string ticket_id, string cus_id, string assined_cus, string ticket_type_id);
        Task cancelTicket(string flight_id, string ticket_id);
        Task paidTicket(Ticket ticket);
        Task orderTicket(Ticket ticket, string cus_id, string assined_cus, string ticket_type_id);
        Task cancelTicket(Ticket ticket);
    }
}
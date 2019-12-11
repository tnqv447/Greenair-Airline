using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence.Repos
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        protected new GreenairContext Context => base.Context as GreenairContext;
        public FlightRepository(GreenairContext context) : base(context)
        {

        }

        private async Task changeFlightStatus(string flight_id, STATUS status)
        {
            try
            {
                var temp = await this.GetByAsync(flight_id);
                temp.Status = status;
                await this.UpdateAsync(temp);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeFlightStatus() Unexpected: " + e);
            }
        }

        public async Task disable(string flight_id)
        {
            await this.changeFlightStatus(flight_id, STATUS.DISABLED);
        }

        public async Task activate(string flight_id)
        {
            await this.changeFlightStatus(flight_id, STATUS.AVAILABLE);
        }

        private async Task<IEnumerable<Flight>> getFlightsByStatus(STATUS status)
        {
            return await this.FindAsync(m => m.Status == status);
        }

        public async Task<IEnumerable<Flight>> getAvailableFlights()
        {
            return await this.getFlightsByStatus(STATUS.AVAILABLE);
        }

        public async Task<IEnumerable<Flight>> getDisabledFlights()
        {
            return await this.getFlightsByStatus(STATUS.DISABLED);
        }

        public async Task<string> getFirstRouteId(string flight_id)
        {
            var sql = await this.getAllFlightDetails(flight_id);
            if (sql == null || sql.Count() == 0) return "";
            sql = sql.OrderBy(m => m.FlightDetailId);
            return sql.FirstOrDefault().RouteId;
        }

        public async Task<string> getLastRouteId(string flight_id)
        {
            var sql = await this.getAllFlightDetails(flight_id);
            if (sql == null || sql.Count() == 0) return "";
            sql = sql.OrderBy(m => m.FlightDetailId);
            return sql.LastOrDefault().RouteId;
        }

        public async Task<DateTime> getArrDate(string flight_id)
        {
            var sql = await this.getAllFlightDetails(flight_id);
            if (sql == null || sql.Count() == 0) return DateTime.MaxValue;
            sql = sql.OrderBy(m => m.FlightDetailId);
            return sql.LastOrDefault().ArrDate;
        }

        public async Task<DateTime> getDepDate(string flight_id)
        {
            var sql = await this.getAllFlightDetails(flight_id);
            if (sql == null || sql.Count() == 0) return DateTime.MinValue;
            sql = sql.OrderBy(m => m.FlightDetailId);
            return sql.FirstOrDefault().DepDate;
        }

        //=========================================================================================================================
        //Thao tac chi tiet chuyen bay
        //=========================================================================================================================
        public async Task<FlightDetail> getFlightDetail(string flight_id, int index)
        {
            try
            {
                if (index < 0) return null;
                var arr = await this.getAllFlightDetails(flight_id);
                if (index >= arr.Count()) return arr.ElementAt(index);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetFlightDetail() Unexpected: " + e);
                return null;
            }
        }
        public async Task<FlightDetail> getFlightDetail(string flight_id, string flightdetail_id)
        {
            try
            {
                var arr = await this.getAllFlightDetails(flight_id);
                foreach (FlightDetail det in arr)
                {
                    if (det.FlightDetailId.Equals(flightdetail_id)) return det;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetFlightDetail() Unexpected: " + e);
                return null;
            }
        }
        public async Task<IEnumerable<FlightDetail>> getAllFlightDetails(string flight_id)
        {
            try
            {
                var sql =
                    from m in this.Context.FlightDetails
                    where m.FlightId.Equals(flight_id)
                    orderby m.FlightDetailId
                    select m;
                return await sql.AsNoTracking().Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetAllFlightDetail() Unexpected: " + e);
                return null;
            }
        }

        public async Task addFlightDetail(FlightDetail det)
        {
            try
            {
                await this.Context.FlightDetails.AddAsync(det);
            }
            catch (Exception e)
            {
                Console.WriteLine("AddFlightDetail() Unexpected: " + e);
            }
        }

        public async Task addFlightDetailRange(IEnumerable<FlightDetail> dets)
        {
            try
            {
                await this.Context.FlightDetails.AddRangeAsync(dets);
            }
            catch (Exception e)
            {
                Console.WriteLine("AddFlightDetailRange() Unexpected: " + e);
            }
        }

        public async Task removeFlightDetail(string flight_id)
        {
            try
            {
                var sql = await this.getAllFlightDetails(flight_id);
                var temp = sql.Last();
                await Task.Run(() => this.Context.FlightDetails.Remove(temp));
            }
            catch (Exception e)
            {
                Console.WriteLine("RemoveFlightDetail() Unexpected: " + e);
            }

        }

        public async Task removeAllFlightDetail(string flight_id)
        {
            try
            {
                var sql = await this.getAllFlightDetails(flight_id);
                await Task.Run(() => this.Context.FlightDetails.RemoveRange(sql));
            }
            catch (Exception e)
            {
                Console.WriteLine("RemoveAllFlightDetail() Unexpected: " + e);
            }
        }

        //=========================================================================================================================
        //Thao tac ve
        //=========================================================================================================================
        public async Task<Ticket> getTicket(string flight_id, string ticket_id)
        {
            try
            {
                var arr = await this.getAllTicketsByFlightId(flight_id);
                foreach (Ticket ticket in arr)
                {
                    if (ticket.TicketId.Equals(ticket_id)) return ticket;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetTicket() Unexpected: " + e);
                return null;
            }
        }
        public async Task<IEnumerable<Ticket>> getAllTickets()
        {
            try
            {
                var sql =
                    from m in this.Context.Tickets
                    orderby m.FlightId
                    select m;
                return await sql.AsNoTracking().Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetAllTicket() Unexpected: " + e);
                return null;
            }
        }
        public async Task<IEnumerable<Ticket>> getAllTicketsByFlightId(string flight_id)
        {
            try
            {
                var sql =
                    from m in this.Context.Tickets
                    where m.FlightId.Equals(flight_id)
                    orderby m.TicketId
                    select m;
                return await sql.AsNoTracking().Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetAllTicket() Unexpected: " + e);
                return null;
            }
        }

        public async Task addTicket(Ticket ticket)
        {
            try
            {
                await this.Context.Tickets.AddAsync(ticket);
            }
            catch (Exception e)
            {
                Console.WriteLine("AddTicket() Unexpected: " + e);
            }
        }

        public async Task addTicketRange(IEnumerable<Ticket> tickets)
        {
            try
            {
                await this.Context.Tickets.AddRangeAsync(tickets);
            }
            catch (Exception e)
            {
                Console.WriteLine("AddTicketRange() Unexpected: " + e);
            }
        }

        private async Task<IEnumerable<Ticket>> getTicketsByStatus(string flight_id, STATUS status)
        {
            try
            {
                var sql =
                    from m in this.Context.Tickets
                    where m.FlightId.Equals(flight_id) && m.Status == status
                    orderby m.TicketId
                    select m;
                return await sql.Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetTicketByStatus() Unexpected: " + e);
                return null;
            }
        }
        public async Task<IEnumerable<Ticket>> getAvailableTickets(string flight_id)
        {
            return await this.getTicketsByStatus(flight_id, STATUS.AVAILABLE);
        }
        public async Task<IEnumerable<Ticket>> getOrderedTickets(string flight_id)
        {
            return await this.getTicketsByStatus(flight_id, STATUS.ORDERED);
        }
        public async Task<IEnumerable<Ticket>> getPaidTickets(string flight_id)
        {
            return await this.getTicketsByStatus(flight_id, STATUS.PAID);
        }

        public async Task<IEnumerable<Ticket>> findTicketAsync(Expression<Func<Ticket, bool>> predicate)
        {
            var tickets = await this.getAllTickets();
            IQueryable<Ticket> res = tickets.AsQueryable();
            return await res.Where(predicate).AsNoTracking().ToListAsync();
        }
        private async Task orderTicketAssignment(Ticket ticket, string cus_id, string assined_cus, string ticket_type_id)
        {
            try
            {
                ticket.CustomerId = cus_id;
                ticket.AssignedCus = assined_cus;
                ticket.TicketTypeId = ticket_type_id;
                this.Context.Tickets.Update(ticket);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("TicketAssignment() Unexpected: " + e);

            }
        }
        private async Task changeTicketStatus(string flight_id, string ticket_id, STATUS status)
        {
            try
            {
                var ticket = await this.getTicket(flight_id, ticket_id);
                ticket.Status = status;
                if (status == STATUS.AVAILABLE)
                {
                    ticket.CustomerId = null;
                    ticket.AssignedCus = null;
                    ticket.TicketTypeId = null;
                }
                this.Context.Tickets.Update(ticket);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeTicketStatus() Unexpected: " + e);

            }
        }
        private async Task changeTicketStatus(Ticket ticket, STATUS status)
        {
            try
            {
                ticket.Status = status;
                if (status == STATUS.AVAILABLE)
                {
                    ticket.CustomerId = null;
                    ticket.AssignedCus = null;
                    ticket.TicketTypeId = null;
                }
                this.Context.Tickets.Update(ticket);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeTicketStatus() Unexpected: " + e);

            }
        }
        public async Task paidTicket(string flight_id, string ticket_id)
        {
            await this.changeTicketStatus(flight_id, ticket_id, STATUS.PAID);
        }
        public async Task orderTicket(string flight_id, string ticket_id, string cus_id, string assined_cus, string ticket_type_id)
        {
            var ticket = await this.getTicket(flight_id, ticket_id);
            await this.orderTicketAssignment(ticket, cus_id, assined_cus, ticket_type_id);
            await this.changeTicketStatus(flight_id, ticket_id, STATUS.ORDERED);
        }
        public async Task cancelTicket(string flight_id, string ticket_id)
        {
            await this.changeTicketStatus(flight_id, ticket_id, STATUS.AVAILABLE);
        }

        public async Task paidTicket(Ticket ticket)
        {
            await this.changeTicketStatus(ticket, STATUS.PAID);
        }
        public async Task orderTicket(Ticket ticket, string cus_id, string assined_cus, string ticket_type_id)
        {
            await this.orderTicketAssignment(ticket, cus_id, assined_cus, ticket_type_id);
            await this.changeTicketStatus(ticket, STATUS.ORDERED);
        }
        public async Task cancelTicket(Ticket ticket)
        {
            await this.changeTicketStatus(ticket, STATUS.AVAILABLE);
        }
    }
}
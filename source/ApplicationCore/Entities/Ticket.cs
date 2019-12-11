
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Interfaces;
using ApplicationCore;

namespace ApplicationCore.Entities
{
    public class Ticket
    {
        public string TicketId { get; set; }

        public string AssignedCus { get; set; }

        public STATUS Status { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }

        public string FlightId { get; set; }
        public Flight Flight { get; set; }

        public Ticket() { }
        public Ticket(string ticket_id, string flight_id, string cus_id, string assinged_cus, string tickettype_id)
        {
            this.TicketId = ticket_id;
            this.FlightId = flight_id;
            this.CustomerId = cus_id;
            this.AssignedCus = assinged_cus;
            this.TicketTypeId = tickettype_id;
        }
        public Ticket(Ticket tic)
        {
            this.TicketId = tic.TicketId;
            this.FlightId = tic.FlightId;
            this.CustomerId = tic.CustomerId;
            this.AssignedCus = tic.AssignedCus;
            this.TicketTypeId = tic.TicketTypeId;
        }
    }
}
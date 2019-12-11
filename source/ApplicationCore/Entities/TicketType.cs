using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
namespace ApplicationCore.Entities
{
    public class TicketType : IAggregateRoot
    {
        public string TicketTypeId { get; set; }

        public string TicketTypeName { get; set; }

        public decimal BasePrice { get; set; }

        public STATUS Status { get; set; }

        public IList<Ticket> Tickets { get; set; }

        public TicketType() { }

        public TicketType(string ticket_type_id, string ticket_type_name, decimal base_price)
        {
            this.TicketTypeId = ticket_type_id;
            this.TicketTypeName = ticket_type_name;
            this.BasePrice = base_price;
        }

        public TicketType(TicketType type)
        {
            this.TicketTypeId = type.TicketTypeId;
            this.TicketTypeName = type.TicketTypeName;
            this.BasePrice = type.BasePrice;
        }
    }
}
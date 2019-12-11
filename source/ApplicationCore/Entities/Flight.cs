using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
namespace ApplicationCore.Entities
{
    public class Flight : IAggregateRoot
    {
        public string FlightId { get; set; }

        public STATUS Status { get; set; }

        public string PlaneId { get; set; }
        public Plane Plane { get; set; }

        public IList<FlightDetail> FlightDetails { get; set; }

        public IList<Ticket> Tickets { get; set; }

        public Flight() { }
        public Flight(string FlightId, string PlaneId, STATUS Status)
        {
            this.FlightId = FlightId;
            this.PlaneId = PlaneId;
            this.Status = Status;
        }
    }
}
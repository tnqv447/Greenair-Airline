using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
namespace ApplicationCore.Entities
{
    public class Plane : IAggregateRoot
    {
        public string PlaneId { get; set; }
        public int SeatNum { get; set; }

        public string MakerId { get; set; }
        public Maker Maker { get; set; }

        public STATUS Status { get; set; }

        public IList<Flight> Flights { get; set; }
        public Plane() { }

        public Plane(string planeid, int seatnum, string makerid)
        {
            this.PlaneId = planeid;
            this.SeatNum = seatnum;
            this.MakerId = makerid;
        }

        public Plane(Plane plane)
        {
            this.PlaneId = plane.PlaneId;
            this.SeatNum = plane.SeatNum;
            this.MakerId = plane.MakerId;
        }
    }
}
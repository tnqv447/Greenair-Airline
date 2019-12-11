using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
namespace ApplicationCore.DTOs
{
    public class FlightDTO
    {
        [StringLength(5, MinimumLength = 5)]
        public string FlightId { get; set; }

        public STATUS Status { get; set; }

        [Required]
        public string PlaneId { get; set; }
        //public PlaneDTO Plane { get; set; }

        //public IList<FlightDetailDTO> FlightDetails { get; set; }

        //public IList<TicketDTO> Tickets { get; set; }
    }
}
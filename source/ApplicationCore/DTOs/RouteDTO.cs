using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
namespace ApplicationCore.DTOs
{
    public class RouteDTO
    {
        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string RouteId { get; set; }

        [Required]
        public string Origin { get; set; }
        //public AirportDTO OrgAirport { get; set; }

        [Required]
        public string Destination { get; set; }
        //public AirportDTO DesAirport { get; set; }

        [Display(Name = "Flight time")]
        public FlightTimeDTO FlightTime { get; set; }

        public STATUS Status { get; set; }

        //public IList<FlightDetailDTO> FlightDetails { get; set; }
    }
}
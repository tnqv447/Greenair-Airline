using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
namespace ApplicationCore.DTOs
{
    public class AirportDTO
    {
        [StringLength(5, MinimumLength = 5)]
        [Required]
        public string AirportId { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string AirportName { get; set; }

        public AddressDTO Address { get; set; }

        public STATUS Status { get; set; }

        // public IList<RouteDTO> RouteStarts { get; set; }

        // public IList<RouteDTO> RouteEnds { get; set; }
    }


}
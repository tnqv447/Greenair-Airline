using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
namespace ApplicationCore.DTOs
{
    public class PlaneDTO
    {
        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string PlaneId { get; set; }

        [Display(Name = "Seats Number")]
        public int SeatNum { get; set; }

        [Required]
        public string MakerId { get; set; }
        //public MakerDTO Maker { get; set; }

        public STATUS Status { get; set; }

        //public IList<FlightDTO> Flights { get; set; }

    }
}
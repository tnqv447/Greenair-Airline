using System.Xml.XPath;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Entities;
namespace ApplicationCore.DTOs
{
    public class FlightDetailDTO
    {

        public FlightDetailDTO(string flightDetailId, string flightId, string routeId, DateTime depDate, DateTime arrDate)
        {
            this.FlightDetailId = flightDetailId;
            this.FlightId = flightId;
            this.RouteId = routeId;
            this.DepDate = depDate;
            this.ArrDate = arrDate;
        }

        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string FlightDetailId { get; set; }


        [Required]
        public string FlightId { get; set; }
        //public FlightDTO Flight { get; set; }

        [Required]
        public string RouteId { get; set; }
        //public RouteDTO Route { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        [Display(Name = "Departure date")]
        public DateTime DepDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        [Display(Name = "Arrive date")]
        public DateTime ArrDate { get; set; }

    }
}
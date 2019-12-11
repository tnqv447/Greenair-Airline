using System.Xml.XPath;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class FlightDetail
    {

        public FlightDetail() { }
        public FlightDetail(string flightDetailId, string flightId, string routeId, DateTime depDate, DateTime arrDate)
        {
            this.FlightDetailId = flightDetailId;
            this.FlightId = flightId;
            this.RouteId = routeId;
            this.DepDate = depDate;
            this.ArrDate = arrDate;
        }

        public string FlightDetailId { get; set; }

        public string FlightId { get; set; }
        public Flight Flight { get; set; }

        public string RouteId { get; set; }
        public Route Route { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime DepDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime ArrDate { get; set; }

    }
}
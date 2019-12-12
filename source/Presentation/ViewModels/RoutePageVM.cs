using Presentation;
using ApplicationCore.DTOs;
using ApplicationCore;

namespace Presentation.ViewModels
{
    public class RoutePageVM
    {
        
        public PaginatedList<RouteVM> Routes { get; set; }
    }
    public class RouteVM{
        public string RouteId { get; set; }
        public string Origin_name { get; set; }
        public string Destination_name { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set;}
        public STATUS Status { get; set; }
        public string FlightTime { get; set; }
        public int Hour { get; set; }
        public int Minute{ get; set; }
        public RouteVM(string RouteId, string Origin_name,string Destination_name, string Origin, string Destination, STATUS status,string FlightTime)
        {
            this.RouteId = RouteId;
            this.Origin_name = Origin_name;
            this.Destination_name = Destination_name;
            this.Origin = Origin;
            this.Destination = Destination;
            this.Status = status;
            this.FlightTime = FlightTime;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Presentation.Helpers;
using Presentation.ViewModels;
using System.IO;
using ApplicationCore.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using ApplicationCore;


namespace Presentation.Pages
{

    public class FlightModel : PageModel
    {
        private readonly IFlightService _flightService;
        [ActivatorUtilitiesConstructor]
        private readonly IUnitOfWork _unitOfWork;
        public FlightModel(IFlightService flightService, IUnitOfWork unitOfWork)
        {
            _flightService = flightService;
            _unitOfWork = unitOfWork;
        }
        public string Msg { get; set; }
        public string CheckType { get; set; }
        public string Check { get; set; }
        public DateTime Check_in { get; set; }
        public DateTime Check_out { get; set; }
        public IEnumerable<FlightDTO> ListFlights_1 { get; set; }
        public IEnumerable<FlightDTO> ListFlights_2 { get; set; }
        public IList<FlightVMIndex> flightVM_1 { get; set; }
        public IList<FlightVMIndex> flightVM_2 { get; set; }

        public async Task OnGetAsync()
        {
            flightVM_1 = new List<FlightVMIndex>();
            flightVM_2 = new List<FlightVMIndex>();
            var FlightSearch = SessionHelper.GetObjectFromJson<Dictionary<string, object>>(HttpContext.Session, "FlightSearch");
            if (FlightSearch != null)
            {
                ViewData["from_city"] = FlightSearch["from_city"];
                ViewData["where_city"] = FlightSearch["where_city"];
                ViewData["from"] = FlightSearch["from"];
                ViewData["where"] = FlightSearch["where"];
                string type = FlightSearch["type"].ToString();
                string vlDepDate = FlightSearch["depdate"].ToString();
                string vlArrDate = FlightSearch["arrdate"].ToString();
                Check_in = DateTime.ParseExact(vlDepDate.ToString(), "dd/MM/yyyy", null);
                DateTime vl_check_in = new DateTime(Check_in.Year, Check_in.Month, Check_in.Day, 0, 0, 0);
                DateTime vl_check_out = new DateTime(Check_out.Year, Check_in.Month, Check_in.Day, 0, 0, 0);

                Check_out = DateTime.ParseExact(vlArrDate, "dd/MM/yyyy", null);
                ViewData["depDate"] = Check_in.ToString("dddd, dd MMMM yyyy");
                ViewData["arrDate"] = Check_out.ToString("dddd, dd MMMM yyyy");
                ViewData["value_dep_date"] = Check_in.ToString("dd/MM/yyyy");
                ViewData["value_arr_date"] = Check_out.ToString("dd/MM/yyyy");
                Msg = Check_in.ToString("dddd dd MMMM yyyy");
                int Adults = Convert.ToInt32(FlightSearch["adults"]);
                int Childs = Convert.ToInt32(FlightSearch["childs"]);
                ViewData["text"] = Adults;
                // DateTime arrDate = DateTime.;
                ListFlights_1 = await _flightService.searchFlightAsync(FlightSearch["from"].ToString()
                   , FlightSearch["where"].ToString(), vl_check_in, Adults, Childs);
                foreach (var item in ListFlights_1)
                {
                    FlightVMIndex vl_flight = new FlightVMIndex();
                    vl_flight.FlightId = item.FlightId;
                    var plane = await _unitOfWork.Planes.GetByAsync(item.PlaneId);
                    var maker = await _unitOfWork.Makers.GetByAsync(plane.MakerId);
                    vl_flight.PlaneId = item.PlaneId;
                    vl_flight.Maker = maker.MakerName;
                    var a = await _flightService.getAllFlightDetailAsync(item.FlightId);
                    // vl_flight.Maker = await _flightService.
                    foreach (var item_2 in a)
                    {
                        vl_flight.FlightDetailId.Add(item_2.FlightDetailId);
                        var dep = item_2.DepDate.ToString("dd-MM-yyyy hh:mm tt");
                        vl_flight.DepDate.Add(dep);
                        var arr = item_2.ArrDate.ToString("dd-MM-yyyy hh:mm tt");
                        vl_flight.ArrDate.Add(arr);
                        var route = await _unitOfWork.Routes.GetByAsync(item_2.RouteId);
                        var airport_origin = await _unitOfWork.Airports.GetByAsync(route.Origin);
                        var airport_destination = await _unitOfWork.Airports.GetByAsync(route.Destination);
                        vl_flight.RouteId.Add(route.RouteId);
                        vl_flight.Origin.Add(airport_origin.AirportName);
                        vl_flight.Destination.Add(airport_destination.AirportName);
                    }
                    vl_flight.Price = await _flightService.calTicketPrice(item.FlightId, "000");
                    flightVM_1.Add(vl_flight);
                }
                if (type == "round")
                {

                    ListFlights_2 = await _flightService.searchFlightAsync(FlightSearch["where"].ToString()
                    , FlightSearch["from"].ToString(), vl_check_out, Adults, Childs);
                    CheckType = "round";
                    foreach (var item in ListFlights_2)
                    {
                        FlightVMIndex vl_flight_2 = new FlightVMIndex();
                        vl_flight_2.FlightId = item.FlightId;
                        vl_flight_2.PlaneId = item.PlaneId;
                        var plane = await _unitOfWork.Planes.GetByAsync(item.PlaneId);
                        var maker = await _unitOfWork.Makers.GetByAsync(plane.MakerId);
                        vl_flight_2.Maker = maker.MakerName;
                        var a = await _flightService.getAllFlightDetailAsync(item.FlightId);
                        foreach (var item_2 in a)
                        {
                            vl_flight_2.FlightDetailId.Add(item_2.FlightDetailId);
                            vl_flight_2.DepDate.Add(item_2.DepDate.ToString("dd-MM-yyyy hh:mm tt"));
                            vl_flight_2.ArrDate.Add(item_2.ArrDate.ToString("dd-MM-yyyy hh:mm tt"));
                            var route = await _unitOfWork.Routes.GetByAsync(item_2.RouteId);
                            var airport_origin = await _unitOfWork.Airports.GetByAsync(route.Origin);
                            var airport_destination = await _unitOfWork.Airports.GetByAsync(route.Destination);
                            vl_flight_2.RouteId.Add(airport_origin.AirportName);
                            vl_flight_2.Origin.Add(airport_destination.AirportName);
                            vl_flight_2.Destination.Add(route.Destination);
                        }
                        vl_flight_2.Price = await _flightService.calTicketPrice(item.FlightId, "000");
                        flightVM_2.Add(vl_flight_2);
                    }
                }
                else
                {
                    CheckType = "one";
                }


            }
        }
        public IActionResult OnGetNewDate(string choose, string type_date, string check)
        {
            var FlightSearch = SessionHelper.GetObjectFromJson<Dictionary<string, object>>(HttpContext.Session, "FlightSearch");
            if (type_date == "check_in")
            {
                if (check == "true")
                {
                    FlightSearch["arrdate"] = choose;
                }
                FlightSearch["depdate"] = choose;
            }
            if (type_date == "check_out")
            {
                FlightSearch["arrdate"] = choose;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "FlightSearch", FlightSearch);

            return new JsonResult(choose);
        }
    }
    // private class Account
    // {
    //     public string Username { get; set; }
    //     public string Password { get; set; }
    // }
}

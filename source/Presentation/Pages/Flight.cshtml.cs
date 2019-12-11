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

namespace Presentation.Pages
{

    public class FlightModel : PageModel
    {
        private readonly IFlightService _flightService;
        [ActivatorUtilitiesConstructor]
        public FlightModel(IFlightService flightService)
        {
            _flightService = flightService;
        }
        public string Msg { get; set; }
        public string CheckType { get; set; }
        public string Check { get; set; }
        public DateTime Check_in { get; set; }
        public DateTime Check_out { get; set; }
        public IEnumerable<FlightDTO> ListFlights_1 { get; set; }
        public IEnumerable<FlightDTO> ListFlights_2 { get; set; }
        private readonly ILogger<FlightModel> _logger;

        public FlightModel(ILogger<FlightModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
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
                   , FlightSearch["where"].ToString(), Check_in, Adults, Childs);
                if (type == "round")
                {

                    ListFlights_2 = await _flightService.searchFlightAsync(FlightSearch["where"].ToString()
                    , FlightSearch["from"].ToString(), Check_out, Adults, Childs);
                    CheckType = "round";
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
            Console.WriteLine(choose);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Presentation.Helpers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        private readonly IUnitOfWork _unitofwork;
        private readonly IAirportService _airportService;
        [ActivatorUtilitiesConstructor]
        public IndexModel(IUnitOfWork unitofwork,IAirportService airportService )
        {
            this._unitofwork = unitofwork;
            this._airportService = airportService;
        }
        [BindProperty]
        public string From { get; set; }
        [BindProperty]
        public string Where { get; set; }
        [Required(ErrorMessage = "You must choose origin!")]
        [BindProperty]
        public string From_City { get; set; }
        [Required(ErrorMessage = "You must choose destination and different the origin!")]
        [BindProperty]
        public string Where_City { get; set; }
        [BindProperty]
        public string DepDate { get; set; }
        [BindProperty]
        public string ArrDate { get; set; }
        [BindProperty]
        public string NumAdults { get; set; }
        
        [BindProperty]
        public string NumChilds { get; set; }
        [BindProperty]
        public string FlightType { get; set; }
        public IEnumerable<Object> ListAirportNames { get; set; }
        public IEnumerable<AirportDTO> ListAirports  { get; set; }
        //public List<FlightDetail> FlightSearch { get; set; }
        public string Msg { get; set; }
        // public IndexModel(ILogger<IndexModel> logger)
        // {
        //     _logger = logger;
        // }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if(From == Where){
                    return Page();
                }
                else{
                    var FlightSearch = new Dictionary<string,object>();
                    FlightSearch.Add("from",From);
                    FlightSearch.Add("where",Where);
                    FlightSearch.Add("from_city",From_City);
                    FlightSearch.Add("where_city",Where_City);
                    FlightSearch.Add("depdate",DepDate);
                    FlightSearch.Add("arrdate",ArrDate);
                    FlightSearch.Add("type",FlightType);
                    FlightSearch.Add("adults",NumAdults);
                    FlightSearch.Add("childs",NumChilds);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "FlightSearch", FlightSearch);
                    return RedirectToPage("Flight");
                }
                
            }
            else{
                return Page();
            }
        }
        public async Task<IActionResult> OnGetAirPortAsync(string term)
        {
                ListAirportNames = await _airportService.searchAirport(term);
                return new JsonResult(ListAirportNames);
        }
        public async Task<IActionResult> OnGetAllAirPortAsync()
        {
            return new JsonResult(await _airportService.getAllAirportAsync());
        }
    }
}
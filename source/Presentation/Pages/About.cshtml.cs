using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Helpers;

namespace Presentation.Pages
{
    public class AboutModel : PageModel
    {
        [BindProperty]
        public string From { get; set; }
        [BindProperty]
        public string Where { get; set; }
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
        private readonly ILogger<AboutModel> _logger;

        
        public AboutModel(ILogger<AboutModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
         public IActionResult OnPost()
        {
            var FlightSearch = new Dictionary<string,object>();
            FlightSearch.Add("from",From);
            FlightSearch.Add("where",Where);
            FlightSearch.Add("depdate",DepDate);
            FlightSearch.Add("arrdate",ArrDate);
            FlightSearch.Add("type",FlightType);
            FlightSearch.Add("adults",NumAdults);
            FlightSearch.Add("childs",NumChilds);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "FlightSearch", FlightSearch);
            return RedirectToPage("Flight");
        }
        
    }
}

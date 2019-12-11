using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Presentation.Helpers;
using ApplicationCore.Entities;
namespace Presentation.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly ILogger<PaymentModel> _logger;
        public int Adults { get; set; }
        public List<Ticket> Cart { get; set; }
        public double Total { get; set; }

        public int Childs { get; set; }
        public PaymentModel(ILogger<PaymentModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Adults = 1;
            var FlightSearch = SessionHelper.GetObjectFromJson<Dictionary<string,object>>(HttpContext.Session,"FlightSearch");
            if(FlightSearch != null)
            {
                Adults = Convert.ToInt32(FlightSearch["adults"]);
                Childs = Convert.ToInt32(FlightSearch["childs"]);
            }
        }
        public IActionResult OnGetCart()
        {   
            Cart = SessionHelper.GetObjectFromJson<List<Ticket>>(HttpContext.Session,"cart");
            return new JsonResult(Cart);
        }
        public IActionResult OnGetChoose()
        {
            return new JsonResult("aaa");
        }
    }
}

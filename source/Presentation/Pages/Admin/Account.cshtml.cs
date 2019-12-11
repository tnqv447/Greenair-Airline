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
namespace Presentation.Pages.Admin
{
    public class AccountModel : PageModel
    {
        private readonly ILogger<AccountModel> _logger;
        public string Msg { get; set; }

        public AccountModel(ILogger<AccountModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public IActionResult OnGetUser()
        {
            string check = "";
            string name = "";
            string job = "";
            if(HttpContext.Session.GetString("username") == null)
            {
                check = "null";
            }
            else
            {
                name = HttpContext.Session.GetString("name");
                job = HttpContext.Session.GetString("job");
                check = "not";
            }
            Dictionary<string,string> Results = new Dictionary<string,string>();
            Results.Add("name",name);
            Results.Add("check",check);
            Results.Add("job",job);
            return new JsonResult(Results);
        }
        public IActionResult OnPostLogout()
        {
            Msg = "";
            HttpContext.Session.Remove("username");
            return new JsonResult(Msg);
        }
    }
}
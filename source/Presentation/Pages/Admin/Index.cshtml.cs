using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Helpers;
using ApplicationCore.Interfaces;
namespace Presentation.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountService _accountService;
        private readonly IUnitOfWork _EmployeeService;
        [ActivatorUtilitiesConstructor]
        // [BindProperty]
        public string Username { get; set; }
        // [BindProperty]
        public string Password { get; set; }
        public string Msg { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IAccountService accountService, IUnitOfWork service)
        {
            _logger = logger;
            _accountService = accountService;
            _EmployeeService = service;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
            Console.WriteLine(Password);
            AccountDTO account = new AccountDTO(Username, Password);
            if (await _accountService.loginCheckAsync(account))
            {
                HttpContext.Session.SetString("username", Username);
                var a = await _accountService.getPersonByAccount(Username);
                if (a.GetType().IsInstanceOfType(new Employee()))
                {
                    var b = await _EmployeeService.Employees.GetByAsync(a.Id);
                    HttpContext.Session.SetString("name", b.FirstName);
                    HttpContext.Session.SetString("job", b.JobId);
                    Msg = "ok";
                    return RedirectToPage("Dashboard");
                }
                else
                {
                    Msg = "You are Customer, not Employee";
                    return Page();
                }
            }
            else
            {
                Msg = "Username or password is incorrect";
                return Page();
            }
            // return Page();
        }
    }
}

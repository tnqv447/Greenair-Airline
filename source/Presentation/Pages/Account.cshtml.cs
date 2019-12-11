using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Helpers;
using Presentation.Services.ServiceInterfaces;

namespace Presentation.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountService _accountService;
        [ActivatorUtilitiesConstructor]
        private readonly ICustomerService _customerService;
        [ActivatorUtilitiesConstructor]
        private readonly IUnitOfWork _unitOfWork;
        public AccountModel(IAccountService accountService, ICustomerService customerService, IUnitOfWork unitOfWork)
        {
            _accountService = accountService;
            _customerService = customerService;
            _unitOfWork = unitOfWork;
        }
        public AccountDTO account { get; set; }
        public CustomerDTO customer { get; set; }
        // private readonly ILogger<AccountModel> _logger;
        public string Msg { get; set; }
        // public AccountModel(ILogger<AccountModel> logger)
        // {
        //     _logger = logger;
        // }

        public IActionResult OnGetUserField()
        {
            
            string userId = "";
            string check = "";
            if( HttpContext.Session.GetString("userid") != null)
            {                
                
                check = "not"; 
                userId = HttpContext.Session.GetString("userid");

            }
            else{   
                check = "null";
            }
            Dictionary<string,string> Results = new Dictionary<string, string>();
            Results.Add("userid",userId);
            Results.Add("check",check);
            return new JsonResult(Results);
        }
        public async Task OnGet()
        {
            string cusId ="";
            string username = "";
            if(HttpContext.Session.GetString("cusid")!= null)
            {
                cusId = HttpContext.Session.GetString("cusid");
                string street ="";
                username = HttpContext.Session.GetString("username");
                
                customer = await _customerService.getCustomerAsync(cusId);
                account = await _accountService.getAccountAsync(username);
                ViewData["username"] = account.Username;
                ViewData["password"] = account.Password;
                ViewData["firstname"] = customer.FirstName;
                ViewData["lastname"] = customer.LastName;
                ViewData["birthdate"] = customer.Birthdate.ToString("dd/MM/yyyy");
                ViewData["phone"] = customer.Phone;
                ViewData["email"] = customer.Email;
                string address = customer.Address.toString();
                string[] listAddress = address.Split(",");
                string[] num_street = listAddress[0].Split(" ");
                if(listAddress.Length == 5 ){
                    ViewData["num"] = num_street[0];
                    for(int i = 1;i<num_street.Length;i++)
                    {
                        street += num_street[i];
                        street += " ";
                    }
                    ViewData["street"] = street;
                    ViewData["district"] = listAddress[1];
                    ViewData["city"] = listAddress[2];
                    ViewData["state"] = listAddress[3];
                    ViewData["country"] = listAddress[4];
                }
                else{
                    ViewData["num"] = num_street[0];
                    for(int i = 1;i<num_street.Length;i++)
                    {
                        street += num_street[i];
                        street += " ";
                    }
                    ViewData["street"] = street;
                    ViewData["district"] = listAddress[1];
                    ViewData["city"] = listAddress[2];
                    ViewData["country"] = listAddress[3];
                }
            }
        
        }
        public async Task<IActionResult> OnPostLogIn()
        {
            string username = "";
            string password = "";
            string userId = "";
            string cusId ="";
            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<Account>(requestBody);
                        if (obj != null)
                        {
                            username = obj.Username;
                            password = obj.Password;
                            AccountDTO account = new AccountDTO(username,password);
                            if(await _accountService.loginCheckAsync(account))
                            {
                                
                                Person Person = await _accountService.getPersonByAccount(username);
                                CustomerDTO customer = await _customerService.getCustomerAsync(Person.Id.ToString());
                                userId = customer.FirstName;
                                cusId = customer.Id;
                                HttpContext.Session.SetString("userid",userId);
                                HttpContext.Session.SetString("username",username);
                                HttpContext.Session.SetString("cusid",cusId);
                                Msg = "true";
                            }
                            else{
                                Msg="False";   
                            }
                        }
                    }
                }
            }
            Dictionary<string, string> lstString = new Dictionary<string, string>();
            lstString.Add("userid", userId);
            lstString.Add("msg", Msg);
            return new JsonResult(lstString);
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("userid");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("cusid");
            return new JsonResult(Msg);
        }
        public async Task<IActionResult> OnPostEditCustomer()
        {
            string cusId ="";
            if(HttpContext.Session.GetString("cusid")!= null)
            {
                cusId = HttpContext.Session.GetString("cusid");
            }
            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<CustomerViewModel>(requestBody);
                        if (obj != null)
                        {
                            CustomerDTO Cus = await _customerService.getCustomerAsync(cusId);
                            AddressDTO address = new AddressDTO(obj.Num, obj.Street, obj.District,obj.City, obj.State, obj.Country);
                            Cus.LastName = obj.LastName;
                            Cus.FirstName = obj.FirstName;
                            Cus.Birthdate = DateTime.ParseExact(obj.Birthdate, "dd/MM/yyyy", null);
                            Cus.Address = address;
                            Cus.Phone = obj.Phone;
                            Cus.Email =obj.Email;
                            await _customerService.updateCustomerAsync(Cus);
                            Msg="Succesfull";
                        }
                    }
                }
            }
            return new JsonResult(Msg);
        }
        public async Task<IActionResult> OnPostEditAccountCustomer()
        {
            string username ="";
            if(HttpContext.Session.GetString("username")!= null)
            {
                username = HttpContext.Session.GetString("username");
            }
            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<AccountDTO>(requestBody);
                        if (obj != null)
                        {
                            AccountDTO account = await _accountService.getAccountAsync(username);
                            account.Password = obj.Password;
                            // await _unitOfWork.CompleteAsync();
                            await _accountService.updateAccountAsync(account);
                            Msg = "Successful";
                            
                        }
                    }
                }
            }
            return new JsonResult(Msg);
        }
    }
    class CustomerViewModel
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Birthdate { get; set; }
        public string Phone { get; set; }
        public string Num { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public CustomerViewModel() { }
    }
}

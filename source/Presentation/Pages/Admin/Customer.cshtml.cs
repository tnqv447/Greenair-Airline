using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ApplicationCore.Entities;
using Presentation.ViewModels;
// using Presentation.Services.ServiceInterfaces;
using Infrastructure.Persistence;
using ApplicationCore.Interfaces;
using ApplicationCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using ApplicationCore.DTOs;
using ApplicationCore.Services;
using Presentation.Services.ServiceInterfaces;
namespace Presentation.Pages.Admin
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _service;
        private readonly ICustomerVMService _serviceVM;
        private readonly IUnitOfWork _unitofwork;
        public STATUS Status { get; set; }

        public CustomerModel(ICustomerService service, ICustomerVMService serviceVM, IUnitOfWork unitofwork)
        {
            this.Status = STATUS.AVAILABLE;
            this._service = service;
            this._serviceVM = serviceVM;
            this._unitofwork = unitofwork;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IEnumerable<CustomerDTO> ListCustomer { get; set; }
        public CustomerPageVM ListCustomerPage { get; set; }
        public async Task OnGet(string searchString, int pageIndex = 1)
        {
            ListCustomerPage = await _serviceVM.GetCustomerPageViewModelAsync(SearchString, pageIndex);
        }

        public async Task<IActionResult> OnGetDetailCustomer(string id)
        {
            var Customer = await _service.getCustomerAsync(id);
            //var Customer = await _unitofwork.Customers.GetByAsync(id);
            Account acc = await _unitofwork.Accounts.getAccountByPersonId(id);
            CustomerVM customerVM = new CustomerVM(Customer, acc);
            return Content(JsonConvert.SerializeObject(customerVM));
            // return new JsonResult(Customer);
        }

        public async Task<IActionResult> OnGetEditCustomer(string id)
        {
            var Customer = await _unitofwork.Customers.GetByAsync(id);
            Account acc = await _unitofwork.Accounts.getAccountByPersonId(Customer.Id);
            CustomerVM customerVM = new CustomerVM(Customer, acc);
            return Content(JsonConvert.SerializeObject(customerVM));
            // return new JsonResult(customerVM);
        }
        public async Task<IActionResult> OnPostEditCustomerLock()
        {
            string respone = "Successful";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);
                    if (obj != null)
                    {
                        string id = obj.Id;
                        await _service.disableCustomerAsync(id);
                        // _service
                    }
                }
            }
            return new JsonResult(respone);
        }
        public async Task<IActionResult> OnPostEditCustomerUnlock()
        {
            string respone = "Successful";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);
                    if (obj != null)
                    {
                        string id = obj.Id;
                        await _service.activateCustomerAsync(id);
                        // _service
                    }
                }
            }
            return new JsonResult(respone);
        }
        public async Task<IActionResult> OnPostDeleteCustomer()
        {
            string CustomerId = "";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);
                    if (obj != null)
                    {
                        CustomerId = obj.Id;
                        await _service.removeCustomerAsync(obj.Id);
                        // await _servic
                    }
                }
            }
            string mes = "Remove " + CustomerId + " Success!";
            return new JsonResult(mes);
        }
    }
    class CustomerVM
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Birthdate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public CustomerVM() { }

        public CustomerVM(string id, string lastname, string firstname, string birthdate, string phone,
        string address, string email, STATUS status)
        {
            this.Id = id; this.LastName = lastname; this.FirstName = firstname; this.Birthdate = birthdate;
            this.Phone = phone; this.Email = email; this.Address = address;
            this.getStatusToString(status);
        }
        public CustomerVM(CustomerDTO cus, Account acc)
        {
            this.Id = cus.Id;
            this.LastName = cus.LastName;
            this.FirstName = cus.FirstName;
            this.Birthdate = cus.Birthdate.ToString();
            this.Phone = cus.Phone; this.Email = cus.Email; this.Address = cus.Address.toString();
            this.getStatusToString(cus.Status);
            this.Username = acc.Username;
            this.Password = acc.Password;
        }
        public CustomerVM(Customer cus, Account acc)
        {
            this.Id = cus.Id;
            this.LastName = cus.LastName;
            this.FirstName = cus.FirstName;
            this.Birthdate = cus.BirthDate.ToString();
            this.Phone = cus.Phone; this.Email = cus.Email; this.Address = cus.Address.toString();
            this.getStatusToString(cus.Status);
            this.Username = acc.Username;
            this.Password = acc.Password;
        }
        public void getStatusToString(STATUS status)
        {
            if (status == STATUS.AVAILABLE)
            {
                this.Status = "AVAILABLE";
            }
            else if (status == STATUS.DISABLED)
            {
                this.Status = "DISABLED";
            }
            else this.Status = null;
        }

    }
}
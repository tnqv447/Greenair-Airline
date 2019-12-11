using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using Newtonsoft.Json;
using Presentation.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using ApplicationCore.DTOs;
using ApplicationCore;
using Presentation.Services.ServiceInterfaces;

namespace Presentation.Pages.Admin
{
    public class EmployeeModel : PageModel
    {
        public STATUS Status { get; set; }
        private readonly IUnitOfWork _unitofwork;
        private readonly IEmployeeVMService _serviceVM;

        public EmployeeModel(IUnitOfWork unitofwork, IEmployeeVMService serviceVM)
        {
            this.Status = STATUS.AVAILABLE;
            // this._service = service;
            this._serviceVM = serviceVM;
            this._unitofwork = unitofwork;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // public IEnumerable<Employee> ListEmployees { get; set; }
        public IEnumerable<Job> ListJobs { get; set; }
        public EmployeePageVM ListEmployeePage { get; set; }
        public async Task OnGet(string searchString, int pageIndex = 1)
        {
            // ListEmployees = await _unitofwork.Employees.GetAllAsync();
            ListJobs = await _unitofwork.Jobs.GetAllAsync();
            ListEmployeePage = await _serviceVM.GetEmployeePageViewModelAsync(SearchString, pageIndex);
        }
        public async Task<IActionResult> OnGetDetailEmployee(string id)
        {
            // var Employee = await _service.getEmployeeAsync(id);
            var Employee = await _unitofwork.Employees.GetByAsync(id);
            Account acc = await _unitofwork.Accounts.getAccountByPersonId(id);
            EmployeeVM EmployeeVM = new EmployeeVM(Employee, acc);
            return Content(JsonConvert.SerializeObject(EmployeeVM));
            // return new JsonResult(Employee);
        }

        public async Task<IActionResult> OnGetEditCustomer(string id)
        {
            var Employee = await _unitofwork.Employees.GetByAsync(id);
            Account acc = await _unitofwork.Accounts.getAccountByPersonId(Employee.Id);
            EmployeeVM EmployeeVM = new EmployeeVM(Employee, acc);
            // return Content(JsonConvert.SerializeObject(customerVM));
            return new JsonResult(EmployeeVM);
        }
        // public async Task<IActionResult> OnPostEditCustomerLock()
        // {
        //     string respone = "Successful";
        //     MemoryStream stream = new MemoryStream();
        //     Request.Body.CopyTo(stream);
        //     stream.Position = 0;
        //     using (StreamReader reader = new StreamReader(stream))
        //     {
        //         string requestBody = reader.ReadToEnd();
        //         if (requestBody.Length > 0)
        //         {
        //             var obj = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);
        //             if (obj != null)
        //             {
        //                 string id = obj.Id;
        //                 await _service.disableCutomerAsync(id);
        //                 // _service
        //             }
        //         }
        //     }
        //     return new JsonResult(respone);
        // }
        // public async Task<IActionResult> OnPostEditCustomerUnlock()
        // {
        //     string respone = "Successful";
        //     MemoryStream stream = new MemoryStream();
        //     Request.Body.CopyTo(stream);
        //     stream.Position = 0;
        //     using (StreamReader reader = new StreamReader(stream))
        //     {
        //         string requestBody = reader.ReadToEnd();
        //         if (requestBody.Length > 0)
        //         {
        //             var obj = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);
        //             if (obj != null)
        //             {
        //                 string id = obj.Id;
        //                 await _service.activateCustomerAsync(id);
        //                 // _service
        //             }
        //         }
        //     }
        //     return new JsonResult(respone);
        // }
        public async Task<IActionResult> OnPostDeleteCustomer()
        {
            string EmployeeId = "";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<EmployeeVM>(requestBody);
                    if (obj != null)
                    {
                        EmployeeId = obj.Id;
                        // await _service.removeCustomerAsync(obj.Id);
                        var em = await _unitofwork.Employees.GetByAsync(EmployeeId);
                        await _unitofwork.Employees.RemoveAsync(em);
                        await _unitofwork.CompleteAsync();
                        // await _servic
                    }
                }
            }
            string mes = "Remove " + EmployeeId + " Success!";
            return new JsonResult(mes);
        }
    }
    class EmployeeVM
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Birthdate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string JobId { get; set; }
        public double Salary { get; set; }
        public string Status { get; set; }
        public EmployeeVM() { }

        public EmployeeVM(string id, string lastname, string firstname, string birthdate, string phone,
        string address, string jobid, double Salary, STATUS status)
        {
            this.Id = id; this.LastName = lastname; this.FirstName = firstname; this.Birthdate = birthdate;
            this.Phone = phone; this.JobId = jobid; this.Salary = Salary; this.Address = address;
            this.getStatusToString(status);
        }
        public EmployeeVM(EmployeeDTO cus, Account acc)
        {
            this.Id = cus.Id;
            this.LastName = cus.LastName;
            this.FirstName = cus.FirstName;
            this.Birthdate = cus.Birthdate.ToString();
            this.Phone = cus.Phone;
            this.JobId = cus.JobId;
            this.Salary = cus.Salary;
            this.Address = cus.Address.toString();
            this.getStatusToString(cus.Status);
            this.Username = acc.Username;
            this.Password = acc.Password;
        }
        public EmployeeVM(Employee cus, Account acc)
        {
            this.Id = cus.Id;
            this.LastName = cus.LastName;
            this.FirstName = cus.FirstName;
            this.Birthdate = cus.BirthDate.ToString();
            this.Phone = cus.Phone;
            this.JobId = cus.JobId;
            this.Salary = cus.Salary;
            this.Address = cus.Address.toString();
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
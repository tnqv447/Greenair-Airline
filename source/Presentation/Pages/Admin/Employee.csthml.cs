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
using ApplicationCore.Services;
using Presentation.Services.ServiceInterfaces;

namespace Presentation.Pages.Admin
{
    public class EmployeeModel : PageModel
    {
        public STATUS Status { get; set; }
        private readonly IUnitOfWork _unitofwork;
        private readonly IEmployeeService _service;
        private readonly IAccountService _accountService;
        private readonly IEmployeeVMService _serviceVM;

        public EmployeeModel(IEmployeeService service, IAccountService accountService, IUnitOfWork unitofwork, IEmployeeVMService serviceVM)
        {
            this.Status = STATUS.AVAILABLE;
            this._service = service;
            this._serviceVM = serviceVM;
            this._unitofwork = unitofwork;
            _accountService = accountService;
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

        public async Task<IActionResult> OnGetEditEmployee(string id)
        {
            var Employee = await _unitofwork.Employees.GetByAsync(id);
            Account acc = await _unitofwork.Accounts.getAccountByPersonId(Employee.Id);
            EmployeeVM EmployeeVM = new EmployeeVM(Employee, acc);
            // return Content(JsonConvert.SerializeObject(EmployeeVM));
            return new JsonResult(EmployeeVM);
        }
        public async Task<IActionResult> OnPostEditEmployeeLock()
        {
            string respone = "Lock Successful";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<EmployeeDTO>(requestBody);
                    if (obj != null)
                    {
                        string id = obj.Id;
                        await _service.disableEmployeeAsync(id);
                        // _service
                    }
                }
            }
            return new JsonResult(respone);
        }
        public async Task<IActionResult> OnPostEditEmployeeUnlock()
        {
            string respone = "Unlock Successful";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<EmployeeDTO>(requestBody);
                    if (obj != null)
                    {
                        string id = obj.Id;
                        await _service.activateEmployeeAsync(id);
                        // _service
                    }
                }
            }
            return new JsonResult(respone);
        }
        public async Task<IActionResult> OnPostDeleteEmployee()
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
                        await _service.removeEmployeeAsync(obj.Id);
                        // await _servic
                    }
                }
            }
            string mes = "Remove " + EmployeeId + " Success!";
            return new JsonResult(mes);
        }
        public async Task<IActionResult> OnPostCreateEmployee()
        {
            string respone = "True";
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
                        var check = await _accountService.isExistedUsernameAsync(obj.Username);
                        if (!check) respone = "False";
                        else
                        {
                            EmployeeDTO emp = new EmployeeDTO();
                            emp.LastName = obj.LastName;
                            emp.FirstName = obj.FirstName;
                            emp.Phone = obj.Phone;
                            emp.Salary = obj.Salary;
                            emp.JobId = obj.JobId;
                            if (obj.Status == "AVAILABLE")
                            {
                                emp.Status = STATUS.AVAILABLE;
                            }
                            else emp.Status = STATUS.DISABLED;
                            AddressDTO address = new AddressDTO();
                            address.toValue(obj.Address);
                            emp.Address = address;
                            await _service.addEmployeeAsync(emp);
                            IEnumerable<EmployeeDTO> Lists = await _service.getAllEmployeeAsync();
                            EmployeeDTO LastEmp = Lists.Last();
                            AccountDTO account = new AccountDTO();
                            account.PersonId = LastEmp.Id;
                            account.Username = obj.Username;
                            account.Password = obj.Password;

                            await _accountService.addAccountAsync(account);
                            // emp.Birthdate=obj.Birthdate;

                        }
                        // _service
                    }
                }
            }
            return new JsonResult(respone);
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
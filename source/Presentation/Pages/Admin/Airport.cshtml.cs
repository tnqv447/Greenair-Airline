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
using Presentation.Services.ServiceInterfaces;

namespace Presentation.Pages.Admin
{
    public class AirportModel : PageModel
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IAirportVMService _services;

        public AirportModel(IAirportVMService services, IUnitOfWork unitofwork)
        {
            this._unitofwork = unitofwork;
            this._services = services;

        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public AirportPageVM ListAirportsPage { get; set; }
        public IEnumerable<Airport> ListAirports { get; set; }
        public async Task OnGet(string searchString, int pageIndex = 1)
        {
            // ListAirports = await _unitofwork.Airports.GetAllAsync();
            ListAirportsPage = await _services.GetAirportPageViewModelAsync(SearchString, pageIndex);
        }
        // Airport methods
        public IActionResult OnGetEditAirport(string id)
        {
            var Airport = _unitofwork.Airports.GetBy(id);
            AirportVM AirportVM = new AirportVM(Airport);
            return Content(JsonConvert.SerializeObject(AirportVM));
            // return new JsonResult(AirportVM);
        }
        public async Task<IActionResult> OnPostEditAirport()
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
                    var obj = JsonConvert.DeserializeObject<AirportVM>(requestBody);
                    if (obj != null)
                    {
                        Address address = new Address();
                        address.toValue(obj.Address);
                        Airport Airport = new Airport();
                        Airport.AirportId = obj.AirportId;
                        Airport.AirportName = obj.AirportName;
                        Airport.Address = address;
                        await _unitofwork.Airports.UpdateAsync(Airport);
                        await _unitofwork.CompleteAsync();
                    }
                }
            }
            return new JsonResult(respone);
        }
        public async Task<IActionResult> OnPostDeleteAirport()
        {
            string AirportId = "";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<AirportVM>(requestBody);
                    if (obj != null)
                    {
                        AirportId = obj.AirportId;
                        var item = await _unitofwork.Airports.GetByAsync(AirportId);
                        await _unitofwork.Airports.RemoveAsync(item);
                        await _unitofwork.CompleteAsync();
                    }
                }
            }
            string mes = "Remove " + AirportId + " Success!";
            return new JsonResult(mes);
        }
        public async Task<IActionResult> OnPostCreateAirport()
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
                    var obj = JsonConvert.DeserializeObject<AirportVM>(requestBody);
                    if (obj != null)
                    {
                        var aa = await _unitofwork.Airports.GetByAsync(obj.AirportId);
                        if (aa == null)
                        {
                            Address address = new Address();
                            address.toValue(obj.Address);
                            Airport Airport = new Airport();
                            Airport.AirportId = obj.AirportId;
                            Airport.AirportName = obj.AirportName;
                            Airport.Address = address;
                            await _unitofwork.Airports.AddAsync(Airport);
                            await _unitofwork.CompleteAsync();
                        }
                        else respone = "False";

                    }
                }
            }
            return new JsonResult(respone);
        }

    }
    public class AirportVM
    {
        public string AirportId { get; set; }
        public string AirportName { get; set; }
        public string Address { get; set; }
        public AirportVM()
        {
        }
        public AirportVM(string id, string name, string Address)
        {
            this.AirportId = id;
            this.AirportName = name;
            this.Address = Address;
        }
        public AirportVM(AirportDTO Airport)
        {
            this.AirportId = Airport.AirportId;
            this.AirportName = Airport.AirportName;
            this.Address = Airport.Address.toString();
        }
        public AirportVM(Airport Airport)
        {
            this.AirportId = Airport.AirportId;
            this.AirportName = Airport.AirportName;
            this.Address = Airport.Address.toString();
        }

    }

}
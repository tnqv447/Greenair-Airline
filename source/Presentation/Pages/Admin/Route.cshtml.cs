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
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.Services;
namespace Presentation.Pages.Admin
{
    public class RouteModel : PageModel
    {
        private readonly IRouteService _services;
        private readonly IUnitOfWork _unitofwork;
        private readonly IRouteVMService _servicesVM;
        public IEnumerable<AirportDTO> Airports { get; set; }
        public RouteModel(IRouteService services, IUnitOfWork unitofwork, IRouteVMService servicesVM)
        {
            _unitofwork = unitofwork;
            _services = services;
            _servicesVM = servicesVM;
        }

        public string SearchString { get; set; }

        public RoutePageVM ListRoutePage { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchRoute { get; set; }


        public async Task OnGet(int pageIndex = 1)
        {
            ListRoutePage = await _servicesVM.GetRoutePageViewModelAsync(SearchRoute, pageIndex);
            Airports = await _servicesVM.GetAllAirport();
        }
        public async Task<IActionResult> OnGetEditRoute(string id)
        {
        //     var Route = await _unitofwork.Routes.GetByAsync(id);
        //     RouteDTO RouteDTO = new RouteDTO();
        //     RouteDTO.RouteId = Route.RouteId;
        //     RouteDTO.SeatNum = Route.SeatNum;
        //     RouteDTO.MakerId = Route.MakerId;
            return Content(JsonConvert.SerializeObject(await _servicesVM.GetRoute(id)));
            // return new JsonResult(await _servicesVM.GetRoute(id));
        }
        public async Task<IActionResult> OnPostEditRoute()
        {
            string respone = "waiting";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<RouteVM>(requestBody);
                    if (obj != null)
                    {
                        FlightTimeDTO flightTime = new FlightTimeDTO(obj.Hour,obj.Minute);
                        
                        RouteDTO Route = new RouteDTO(obj.RouteId,obj.Origin,obj.Destination,flightTime,obj.Status);
                        await _servicesVM.UpdateRoute(Route);
                        respone = "successful";
                    }
                }
            }
            return new JsonResult(respone);
        }
        public async Task<IActionResult> OnPostDeleteRoute()
        {
            
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<RouteVM>(requestBody);
                    if (obj != null)
                    {
                        await _servicesVM.RemoveRoute(obj.RouteId);
                    }
                }
            }
            string mes = "Remove  Success!";
            return new JsonResult(mes);
        }
        public async Task<IActionResult> OnPostCreateRoute()
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
                    var obj = JsonConvert.DeserializeObject<RouteVM>(requestBody);
                    if (obj != null)
                    {
                        FlightTimeDTO flightTime = new FlightTimeDTO(obj.Hour,obj.Minute);
                        RouteDTO Route = new RouteDTO();
                        Route.Origin = obj.Origin;
                        Route.Destination = obj.Destination;
                        Route.FlightTime = flightTime;
                        Route.Status = obj.Status;
                        // Route.RouteId = obj.RouteId;
                        await _servicesVM.AddRoute(Route);

                    }
                }
            }
            return new JsonResult(respone);
        }

    }
}
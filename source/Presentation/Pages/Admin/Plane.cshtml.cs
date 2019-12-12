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
    public class PlaneModel : PageModel
    {
        private readonly IPlaneService _services;
        private readonly IUnitOfWork _unitofwork;
        private readonly IPlaneVMService _servicesVM;

        public PlaneModel(IPlaneService services, IUnitOfWork unitofwork, IPlaneVMService servicesVM)
        {
            this._unitofwork = unitofwork;
            this._services = services;
            this._servicesVM = servicesVM;
        }

        public string SearchString { get; set; }
        public IEnumerable<Maker> ListMakers { get; set; }
        public SelectList ListMakerName { get; set; }

        public PlanePageVM ListPlanePage { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchMaker { get; set; }

        public async Task OnGet(int pageIndex = 1)
        {
            ListMakers = await _unitofwork.Makers.GetAllAsync();
            ListPlanePage = await _servicesVM.GetPlanePageViewModelAsync(SearchMaker, pageIndex);
            List<string> listname = new List<string>();
            foreach (var item in ListMakers)
            {
                listname.Add(item.MakerId + " - " + item.MakerName);
            }
            ListMakerName = new SelectList(listname);
        }
        public async Task<IActionResult> OnGetEditPlane(string id)
        {
            var plane = await _unitofwork.Planes.GetByAsync(id);
            PlaneDTO planeDTO = new PlaneDTO();
            planeDTO.PlaneId = plane.PlaneId;
            planeDTO.SeatNum = plane.SeatNum;
            planeDTO.MakerId = plane.MakerId;
            return Content(JsonConvert.SerializeObject(planeDTO));
            // return new JsonResult(planeDTO);
        }
        public async Task<IActionResult> OnPostEditPlane()
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
                    var obj = JsonConvert.DeserializeObject<PlaneDTO>(requestBody);
                    if (obj != null)
                    {
                        Plane plane = new Plane();
                        plane.PlaneId = obj.PlaneId;
                        plane.SeatNum = obj.SeatNum;
                        plane.MakerId = obj.MakerId.Substring(0, 3);
                        await _unitofwork.Planes.UpdateAsync(plane);
                        await _unitofwork.CompleteAsync();
                    }
                }
            }
            return new JsonResult(respone);
        }
        public async Task<IActionResult> OnPostDeletePlane()
        {
            string PlaneId = "";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<PlaneDTO>(requestBody);
                    if (obj != null)
                    {
                        await _services.disablePlaneAsync(obj.PlaneId);
                    }
                }
            }
            string mes = "Remove " + PlaneId + " Success!";
            return new JsonResult(mes);
        }
        public async Task<IActionResult> OnPostCreatePlane()
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
                    var obj = JsonConvert.DeserializeObject<PlaneDTO>(requestBody);
                    if (obj != null)
                    {
                        PlaneDTO plane = new PlaneDTO();
                        // plane.PlaneId = obj.PlaneId;
                        plane.SeatNum = obj.SeatNum;
                        plane.MakerId = obj.MakerId.Substring(0, 3);
                        await _services.addPlaneAsync(plane);

                    }
                }
            }
            return new JsonResult(respone);
        }

    }
}
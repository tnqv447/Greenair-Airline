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
    public class MakerModel : PageModel
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMakerVMService _services;

        public MakerModel(IUnitOfWork unitofwork, IMakerVMService services)
        {
            this._unitofwork = unitofwork;
            this._services = services;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IEnumerable<Maker> ListMakers { get; set; }
        // public IEnumerable<Plane> ListPlanes { get; set; }

        public MakerPageVM ListMakerPage { get; set; }

        public async Task OnGet(string searchString, int pageIndex = 1)
        {
            ListMakers = await _unitofwork.Makers.GetAllAsync();
            // ListPlanes = await _unitofwork.Planes.GetAllAsync();
            ListMakerPage = await _services.GetMakerPageViewModelAsync(SearchString, pageIndex);

        }
        // Maker methods
        public IActionResult OnGetEditMaker(string id)
        {
            var maker = _unitofwork.Makers.GetBy(id);
            MakerVM makerVM = new MakerVM(maker);
            return Content(JsonConvert.SerializeObject(makerVM));
            // return new JsonResult(makerVM);
        }
        public async Task<IActionResult> OnPostEditMaker()
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
                    var obj = JsonConvert.DeserializeObject<MakerVM>(requestBody);
                    if (obj != null)
                    {
                        Address address = new Address();
                        address.toValue(obj.Address);
                        Maker maker = new Maker();
                        maker.MakerId = obj.MakerId;
                        maker.MakerName = obj.MakerName;
                        maker.Address = address;
                        await _unitofwork.Makers.UpdateAsync(maker);
                        await _unitofwork.CompleteAsync();
                    }
                }
            }
            return new JsonResult(respone);
        }
        public async Task<IActionResult> OnPostDeleteMaker()
        {
            string MakerId = "";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<MakerVM>(requestBody);
                    if (obj != null)
                    {
                        MakerId = obj.MakerId;
                        var item = await _unitofwork.Makers.GetByAsync(MakerId);
                        await _unitofwork.Makers.RemoveAsync(item);
                        await _unitofwork.CompleteAsync();
                    }
                }
            }
            string mes = "Remove " + MakerId + " Success!";
            return new JsonResult(mes);
        }
        public async Task<IActionResult> OnPostCreateMaker()
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
                    var obj = JsonConvert.DeserializeObject<MakerVM>(requestBody);
                    if (obj != null)
                    {
                        var aa = await _unitofwork.Makers.GetByAsync(obj.MakerId);
                        if (aa == null)
                        {
                            Address address = new Address();
                            address.toValue(obj.Address);
                            Maker Maker = new Maker();
                            Maker.MakerId = null;
                            Maker.MakerName = obj.MakerName;
                            Maker.Address = address;
                            await _unitofwork.Makers.AddAsync(Maker);
                            await _unitofwork.CompleteAsync();
                        }
                        else respone = "False";

                    }
                }
            }
            return new JsonResult(respone);
        }

    }
    public class MakerVM
    {
        public string MakerId { get; set; }
        public string MakerName { get; set; }
        public string Address { get; set; }
        public MakerVM()
        {
        }
        public MakerVM(string id, string name, string Address)
        {
            this.MakerId = id;
            this.MakerName = name;
            this.Address = Address;
        }
        public MakerVM(MakerDTO maker)
        {
            this.MakerId = maker.MakerId;
            this.MakerName = maker.MakerName;
            this.Address = maker.Address.toString();
        }
        public MakerVM(Maker maker)
        {
            this.MakerId = maker.MakerId;
            this.MakerName = maker.MakerName;
            this.Address = maker.Address.toString();
        }
    }
}
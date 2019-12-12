using System;
using System.Collections.Generic;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.ViewModels
{
    public class FlightVMIndex
    {
        public string FlightId { get; set; }
        public string PlaneId { get; set; }
        public List<string> FlightDetailId { get; set; }
        public List<string> RouteId { get; set; }
        public List<string> DepDate { get; set; }
        public List<string> ArrDate { get; set; }
        public List<string> Origin { get; set; }
        public List<string> Destination { get; set; }
        public Decimal Price { get; set; }
        public string Maker { get; set; }
        public FlightVMIndex()
        {
            FlightDetailId = new List<string>();
            RouteId = new List<string>();
            DepDate = new List<string>();
            ArrDate = new List<string>();
            Origin = new List<string>();
            Destination = new List<string>();
        }
    }
}
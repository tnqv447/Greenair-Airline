using System;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.ViewModels
{
    public class FlightVM
    {
        public FlightDTO Flights { get; set;}
    }
}
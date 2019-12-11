using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using System;

namespace ApplicationCore.DTOs
{
    public class CustomerDTO : PersonDTO
    {
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" +
            "@" +
            @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Email không đúng")]
        [Required]
        public string Email { get; set; }
        //public IList<TicketDTO> Tickets { get; set; }
    }
}
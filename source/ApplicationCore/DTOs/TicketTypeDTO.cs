using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.DTOs
{
    public class TicketTypeDTO
    {

        [StringLength(3, MinimumLength = 3)]
        [Required]
        public string TicketTypeId { get; set; }

        [StringLength(10, MinimumLength = 5)]
        public string TicketTypeName { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }

        public STATUS Status { get; set; }

        //public IList<TicketDTO> Tickets { get; set; }
    }
}
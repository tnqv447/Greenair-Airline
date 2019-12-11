
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Interfaces;
using ApplicationCore;

namespace ApplicationCore.DTOs
{
    public class TicketDTO
    {
        [StringLength(5, MinimumLength = 5)]
        [RegularExpression("^[A-Z][0-9]{4}$")]
        [Required]
        public string TicketId { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string AssignedCus { get; set; }

        public STATUS Status { get; set; }

        [Required]
        public string CustomerId { get; set; }
        //public CustomerDTO Customer { get; set; }

        [Required]
        public string TicketTypeId { get; set; }
        //public TicketTypeDTO TicketType { get; set; }

        [Required]
        public string FlightId { get; set; }
        //public FlightDTO Flight { get; set; }

    }
}
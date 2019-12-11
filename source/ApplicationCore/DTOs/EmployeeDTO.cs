using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
namespace ApplicationCore.DTOs
{
    public class EmployeeDTO : PersonDTO
    {
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public double Salary { get; set; }

        [Required]
        public string JobId { get; set; }
        //public JobDTO Job { get; set; }
    }
}
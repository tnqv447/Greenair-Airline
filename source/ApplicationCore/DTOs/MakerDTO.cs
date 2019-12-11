
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ApplicationCore.Interfaces;

namespace ApplicationCore.DTOs
{
    public class MakerDTO
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]

        public string MakerId { get; set; }

        [StringLength(20, MinimumLength = 5)]
        public string MakerName { get; set; }

        public AddressDTO Address { get; set; }

        public STATUS Status { get; set; }

        //public IList<PlaneDTO> Planes { get; set; }

        public MakerDTO(string makerId, string makerName, AddressDTO address)
        {
            this.MakerId = makerId;
            this.MakerName = makerName;
            this.Address = address;

        }
        public MakerDTO(string makerId, string makerName, string address)
        {
            this.MakerId = makerId;
            this.MakerName = makerName;
            this.Address.toValue(address);
        }
    }


}
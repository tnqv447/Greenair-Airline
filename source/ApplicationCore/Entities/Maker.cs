
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class Maker : IAggregateRoot
    {
        public string MakerId { get; set; }

        public string MakerName { get; set; }

        public Address Address { get; set; }

        public STATUS Status { get; set; }

        public IList<Plane> Planes { get; set; }

        public Maker() { }

        public Maker(string makerId, string makerName, Address address)
        {
            this.MakerId = makerId;
            this.MakerName = makerName;
            this.Address = address;

        }

        public Maker(string makerId, string makerName, string address)
        {
            this.MakerId = makerId;
            this.MakerName = makerName;
            this.Address.toValue(address);
        }
    }


}
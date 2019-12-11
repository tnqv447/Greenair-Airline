using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ApplicationCore.Interfaces;
using System;
namespace ApplicationCore.Entities
{
    public class Customer : Person, IAggregateRoot
    {
        public Customer() : base() { }

        public Customer(string id, string lastName, string firstName, DateTime birthDate, string phone, Address address,
                STATUS status, Account account, string email) : base(id, lastName, firstName, birthDate, phone, address, status, account)
        {
            this.Email = email;

        }

        public Customer(string id, string lastName, string firstName, DateTime birthDate, string phone, Address address, Account account,
                string email) : base(id, lastName, firstName, birthDate, phone, address, account)
        {
            this.Email = email;

        }
        public string Email { get; set; }

        public IList<Ticket> Tickets { get; set; }

    }
}
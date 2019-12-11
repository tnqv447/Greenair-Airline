using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Interfaces;
namespace ApplicationCore.Entities
{
    public class Account : IAggregateRoot
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public STATUS Status { get; set; }

        public string PersonId { get; set; }
        public Person Person { get; set; }

        public Account() { }

        public Account(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public Account(Account acc)
        {
            this.Username = acc.Username;
            this.Password = acc.Password;
        }
    }
}
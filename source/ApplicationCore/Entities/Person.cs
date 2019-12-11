using System.ComponentModel.DataAnnotations;
using ApplicationCore.Interfaces;
using ApplicationCore;
using System;
namespace ApplicationCore.Entities
{
    public class Person : IAggregateRoot
    {
        public Person() { }
        public Person(string id, string lastName, string firstName, DateTime birthDate, string phone, Address address, STATUS status, Account account)
        {
            this.Id = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.BirthDate = birthDate;
            this.Phone = phone;
            this.Address = address;
            this.Status = status;
            this.Account = account;

        }
        public Person(string id, string lastName, string firstName, DateTime birthDate, string phone, Address address, Account account)
        {
            this.Id = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.BirthDate = birthDate;
            this.Phone = phone;
            this.Address = address;
            this.Account = account;

        }
        public string Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }

        public Account Account { get; set; }

        public STATUS Status { get; set; }


    }

}
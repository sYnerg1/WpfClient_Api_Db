using DataApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataApi.BAL.DTOs
{
    public class PersonDTO
    {
        public PersonDTO()
        {
            Contacts = new List<ContactDTO>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Cpny { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime FirstContact { get; set; }

        public string CountryCode { get; set; }
        public string Country1 { get; set; }
        public string Country2 { get; set; }
        public string Country3 { get; set; }
        public string Country4 { get; set; }

        public int GreetingId { get; set; }
        public string Greeting1 { get; set; }
        public string Greeting2{ get; set; }
        public string Greeting3 { get; set; }
        public string Greeting4 { get; set; }

        public string Contact { get; set; }

        public List<ContactDTO> Contacts { get; set; }

    }
}

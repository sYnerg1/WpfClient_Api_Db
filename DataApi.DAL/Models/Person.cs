﻿using System;
using System.Collections.Generic;

namespace DataApi.DAL.Models
{
    public partial class Person
    {
        public Person()
        {
            PersonContact = new List<PersonContact>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Cpny { get; set; }
        public string Street { get; set; }
        public string CountryCode { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public byte GenderId { get; set; }
        public int GreetingId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime FirstContact { get; set; }
        public string Notes { get; set; }


        public virtual Country CountryCodeNavigation { get; set; }
        public virtual Greeting Greeting { get; set; }
        public virtual List<PersonContact> PersonContact { get; set; }
    }
}

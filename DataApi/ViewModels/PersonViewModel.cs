using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApi.ViewModels
{
    public class PersonViewModel
    {
        public PersonViewModel()
        {

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

        public string Country { get; set; }
        public string Greeting { get; set; }
        public string Contact { get; set; }
    }
}

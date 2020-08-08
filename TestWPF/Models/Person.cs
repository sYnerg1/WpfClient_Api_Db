﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestWPF.Models
{
    public class Person : INotifyPropertyChanged
    {
        public Person()
        {

        }

        [JsonIgnore]
        private string greetingView;
        [JsonIgnore]
        private string countryView;

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

        public string Country1 { get; set; }
        public string Country2 { get; set; }
        public string Country3 { get; set; }
        public string Country4 { get; set; }
        [JsonIgnore]
        public string CountryView
        {
            get { return countryView; }
            set
            {
                countryView = value;
                OnPropertyChanged("CountryView");
            }
        } 

        public string Greeting1 { get; set; }
        public string Greeting2 { get; set; }
        public string Greeting3 { get; set; }
        public string Greeting4 { get; set; }
        [JsonIgnore]
        public string GreetingView 
        {
            get { return greetingView; }
            set
            {
                greetingView = value;
                OnPropertyChanged("GreetingView");
            }
        }

        public string Contact { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

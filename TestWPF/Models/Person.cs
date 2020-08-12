using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Validation;

namespace TestWPF.Models
{
    public class Person : INotifyPropertyChanged/*,IDataErrorInfo*/
    {
        public Person()
        {
            Contacts = new List<Contact>();
        }
        private string title;
        private string fname;
        private string lname;
        private string cpny;
        private string street;
        private string zip;
        private string city;
        private int greetingId;

        [JsonIgnore]
        private string greetingView;
        [JsonIgnore]
        private string countryView;

        public int Id { get; set; }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Fname
        {
            get { return fname; }
            set
            {
                fname = value;
                OnPropertyChanged("Fname");
            }
        }
        public string Lname
        {
            get { return lname; }
            set
            {
                lname = value;
                OnPropertyChanged("Lname");
            }
        }
        public string Cpny
        {
            get { return cpny; }
            set
            {
                cpny = value;
                OnPropertyChanged("Cpny");
            }
        }
        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }
        public string Zip
        {
            get { return zip; }
            set
            {
                zip = value;
                OnPropertyChanged("Zip");
            }
        }
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        public DateTime? DateOfBirth { get; set; }
        public DateTime FirstContact { get; set; }

        public string CountryCode { get; set; }
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

        public int GreetingId
        {
            get { return greetingId; }
            set
            {
                greetingId = value;
                OnPropertyChanged("GreetingId");
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

        public List<Contact> Contacts { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

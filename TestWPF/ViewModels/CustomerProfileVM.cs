using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Commands;
using TestWPF.Models;

namespace TestWPF.ViewModels
{
    public class CustomerProfileVM : INotifyPropertyChanged
    {
        HttpClient _client;
        public CustomerProfileVM()
        {
            Contacts = new ObservableCollection<Contact>();
            person = new Person();
            selectedContact = new Contact();

        }

        public CustomerProfileVM(Person personFromDb)
        {
            person = personFromDb;
            Contacts = new ObservableCollection<Contact>(personFromDb.Contacts);

            selectedContact = new Contact();

            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5000/api/");
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        private Person person;
        public  Person Person
        {
            get { return person; }
            set
            {
                person = value;
                OnPropertyChanged("Person");
            }
        }

        private Contact selectedContact;
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged1;

        public void RaisePropertyChanged(string propertyName)
        {
            var pc = PropertyChanged1;
            if (pc != null)
                pc(this, new PropertyChangedEventArgs(propertyName));
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(Delete));
            }
        }

        public async void Delete(object obj)
        {
            var castToIList = obj as IList;
            if (obj != null)
            {
                var listOfSelectedItems = castToIList.OfType<Contact>().ToList();

                foreach (Contact c in listOfSelectedItems)
                {
                    Contacts.Remove(c);
                }
            }
        }

        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return refreshCommand ??
                  (refreshCommand = new RelayCommand(Refresh));
            }
        }

        public async void Refresh(object obj)
        {
            HttpResponseMessage response = await _client.GetAsync($"person/{person.Id}");

            var jsonCustomer = await response.Content.ReadAsStringAsync();

            var customer = JsonConvert.DeserializeObject<Person>(jsonCustomer);

            Contacts.Clear();

            foreach(Contact c in customer.Contacts)
            {
                Contacts.Add(c);
            }
        }
    }
}

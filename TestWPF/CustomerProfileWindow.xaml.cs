using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestWPF.Models;
using TestWPF.ViewModels;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for CustomerProfileWindow.xaml
    /// </summary>
    public partial class CustomerProfileWindow : Window
    {

        public event Action<int> CustomerProfileUpdated;

        private readonly List<Greeting> _greetings;
        private readonly List<Country> _countries;
        private readonly int _languageCode;
        private readonly int _customerId;
        private HttpClient _client = new HttpClient();
       

        private CustomerProfileVM _customerVM;

        public CustomerProfileWindow(int customerId,
            int languageCode,
            List<Country> countries,
            List<Greeting> greetings)
        {
            _languageCode = languageCode;
            _customerId = customerId;
            _greetings = greetings;
            _countries = countries;

            InitializeComponent();

            _client.BaseAddress = new Uri("http://localhost:5000/api/");
           
            this.Loaded += Window_Loaded_For_Edit_Customer;
        }


        private async void Window_Loaded_For_Edit_Customer(object sender, RoutedEventArgs e)
        {
            var response = await _client.GetAsync($"person/{_customerId}");

            var jsonCustomer = await response.Content.ReadAsStringAsync();

            var customer = JsonConvert.DeserializeObject<Person>(jsonCustomer);

            _customerVM = new CustomerProfileVM(customer);

            DataContext = _customerVM;
            int personCountryOrder = 0;
            foreach(Country c in _countries)
            {
                if(c.Code==customer.CountryCode)
                {
                    break;
                }
                personCountryOrder++;
            }                    

            switch (_languageCode)
            {
                case 0:
                    greetingBox.ItemsSource = _greetings.Select(x => x.Txt1);
                    countryBox.ItemsSource = _countries.Select(x => x.Txt1);
                    break;
                case 1:
                    greetingBox.ItemsSource = _greetings.Select(x => x.Txt2);
                    countryBox.ItemsSource = _countries.Select(x => x.Txt2);
                    break;
                case 2:
                    greetingBox.ItemsSource = _greetings.Select(x => x.Txt3);
                    countryBox.ItemsSource = _countries.Select(x => x.Txt3);
                    break;
                case 3:
                    greetingBox.ItemsSource = _greetings.Select(x => x.Txt4);
                    countryBox.ItemsSource = _countries.Select(x => x.Txt4);
                    break;
            }

            countryBox.SelectedIndex = personCountryOrder;
            greetingBox.SelectedIndex = customer.GreetingId - 1;
        }

        private void Edit_Contacts_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow window = new ContactWindow(_customerVM.SelectedContact);
            window.ShowDialog();
        }

        private void NewContact_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow window = new NewContactWindow();
            window.ContactCreated += SaveContact;

            if (window.ShowDialog() == true)
            {
                MessageBox.Show("Added");
            }
        }

        public void SaveContact(Contact contact)
        {
            _customerVM.Contacts.Add(contact);
        }

        private void CustumerProfile_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private async void CustumerProfile_Save_Click(object sender, RoutedEventArgs e)
        {
            if ((_customerVM.Person.Lname == "" || _customerVM.Person.Lname == null) ||
                (_customerVM.Person.Fname == "" || _customerVM.Person.Fname == null) ||
                (_customerVM.Person.City == "" || _customerVM.Person.City == null))
            {
                MessageBox.Show("Fields:\nLast name\nFirst name\nCity\nAre Required!!!");
            }
            else
            {
                //set new country txt
                _customerVM.Person.CountryCode = _countries[countryBox.SelectedIndex].Code;

                //set new cgreeting txt
                _customerVM.Person.GreetingId = greetingBox.SelectedIndex + 1;

                _customerVM.Person.Contacts = _customerVM.Contacts.ToList();

                if (_customerVM.Person.Contacts.Any(x=>x.ContactTypeId==1))
                {
                    _customerVM.Person.Contact = _customerVM.Person.Contacts.FirstOrDefault(x=>x.ContactTypeId==1).Txt; 
                }
                else
                {
                    _customerVM.Person.Contact = null;
                }

                string json = JsonConvert.SerializeObject(_customerVM.Person);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync($"person/{_customerVM.Person.Id}", data);
                if (response.IsSuccessStatusCode)
                {
                    int  idOfUpdatedPerson = Convert.ToInt32(await response.Content.ReadAsStringAsync());
                    CustomerProfileUpdated(idOfUpdatedPerson);
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show($"{response.Content}");
                }
            }
        }

        private void EditContact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow window = new ContactWindow(_customerVM.SelectedContact);
            window.ShowDialog();
        }
    }
}

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
    /// Interaction logic for NewCustomerProfileWindow.xaml
    /// </summary>
    public partial class NewCustomerProfileWindow : Window
    {
        public event Action<Person> CustomerProfileCreated;

        private readonly IEnumerable<Greeting> _greetings;
        private readonly List<Country> _countries;
        private readonly int _languageCode;

        private CustomerProfileVM _newCustomer;

        private HttpClient _client = new HttpClient();


        public NewCustomerProfileWindow(List<Country> countries,IEnumerable<Greeting> greeetings, int languageCode)
        {
            _newCustomer = new CustomerProfileVM();
            _greetings = greeetings;
            _countries = countries;
            _languageCode = languageCode;
            DataContext = _newCustomer;
            
            InitializeComponent();

            this.Loaded += Window_Loaded;

            _client.BaseAddress = new Uri("http://localhost:5000/api/"); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch(_languageCode)
            {
                case 0:
                    greetingBox.ItemsSource = _greetings.Select(x=>x.Txt1);
                    countryBox.ItemsSource = _countries.Select(x=>x.Txt1);
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
        }

        private async void NewCustumerProfile_Save_Click(object sender, RoutedEventArgs e)
        {
            _newCustomer.Person.CountryCode = _countries[countryBox.SelectedIndex].Code;
            _newCustomer.Person.FirstContact = DateTime.Now;
            _newCustomer.Person.Contacts = _newCustomer.Contacts.ToList();
            string json = JsonConvert.SerializeObject(_newCustomer.Person);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("person", data);
            response.EnsureSuccessStatusCode();

            CustomerProfileCreated(_newCustomer.Person);

            this.DialogResult = true;
        }

        private void NewCustumerProfile_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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
            _newCustomer.Contacts.Add(contact);
        }
    }
}

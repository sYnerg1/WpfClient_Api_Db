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

        public event Action<Person> CustomerProfileUpdate;
        private  readonly int _customerId;
        private readonly IEnumerable<Greeting> _greetings;
        private int _languageCode;

        CustomerProfileVM customer;

        private HttpClient _client = new HttpClient();

        public CustomerProfileWindow(int customerId,int languageCode)
        {
            //    DataContext = person;
            _customerId = customerId;

            InitializeComponent();

            _client.BaseAddress = new Uri("http://localhost:5000/api/");
            _languageCode = languageCode;
            this.Loaded += Window_Loaded_For_Edit_Customer;
        }


        private async void Window_Loaded_For_Edit_Customer(object sender, RoutedEventArgs e)
        {
            var countryNamesResponse = await _client.GetAsync($"person/{_customerId}");

            var countryNamesJson = await countryNamesResponse.Content.ReadAsStringAsync();

            var countryNames = JsonConvert.DeserializeObject<IEnumerable<string>>(countryNamesJson);

            countryBox.ItemsSource = countryNames;
        }

        private void Edit_Contacts_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow window = new ContactWindow();
            window.ShowDialog();
        }

        private void DeleteContacts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void New_Contact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow window = new ContactWindow();
            window.ShowDialog();
        }

        private void Save_CustomerProfile_Click(object sender, RoutedEventArgs e)
        {
            CustomerProfileUpdate(new Person());
        }
    }
}

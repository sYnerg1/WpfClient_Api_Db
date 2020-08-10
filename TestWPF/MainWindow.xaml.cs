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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestWPF.Models;
using TestWPF.ViewModels;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Greeting> Greetings;
        private static List<Country> Countries;

        PersonViewModel persons;
        HttpClient client = new HttpClient();

        public MainWindow()
        {

            persons = new PersonViewModel();
            DataContext = persons;
            
            client.BaseAddress = new Uri("http://localhost:5000/api/");
            this.Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        //main_Window start
        async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = await client.GetAsync("/api/person");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var personsFromApi = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);

            foreach (Person p in personsFromApi)
            {
                persons.Persons.Add(p);
            }

            HttpResponseMessage responseGreeting = await client.GetAsync("/api/greeting");
            responseGreeting.EnsureSuccessStatusCode();
            var jsonGreeting = await responseGreeting.Content.ReadAsStringAsync();

            Greetings = JsonConvert.DeserializeObject<List<Greeting>>(jsonGreeting);

            foreach(Greeting gr in Greetings)
            {
                if (gr.Txt2 == null || gr.Txt2 == "")
                {
                    gr.Txt2 = gr.Txt1;
                }
                if (gr.Txt3 == null || gr.Txt3 == "")
                {
                    gr.Txt3 = gr.Txt1;
                }
                if (gr.Txt4 == null || gr.Txt4 == "")
                {
                    gr.Txt4 = gr.Txt1;
                }
            }

            HttpResponseMessage responseCountry = await client.GetAsync("/api/country");
            responseCountry.EnsureSuccessStatusCode();
            var jsonCountry = await responseCountry.Content.ReadAsStringAsync();

            Countries = JsonConvert.DeserializeObject<List<Country>>(jsonCountry);

            foreach (Country gr in Countries)
            {
                if (gr.Txt2 == null || gr.Txt2 == "")
                {
                    gr.Txt2 = gr.Txt1;
                }
                if (gr.Txt3 == null || gr.Txt3 == "")
                {
                    gr.Txt3 = gr.Txt1;
                }
                if (gr.Txt4 == null || gr.Txt4 == "")
                {
                    gr.Txt4 = gr.Txt1;
                }
            }

        }


        //language change with combobox 
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (languageSelector.SelectedIndex)
            {
                case 0:
                    foreach (Person p in ((PersonViewModel)DataContext).Persons)
                    {
                        p.GreetingView = p.Greeting1;
                        p.CountryView = p.Country1;
                    }
                    break;
                case 1:
                    foreach (Person p in ((PersonViewModel)DataContext).Persons)
                    {
                        p.GreetingView = p.Greeting2;
                        p.CountryView = p.Country2;
                    }
                    break;
                case 2:
                    foreach (Person p in ((PersonViewModel)DataContext).Persons)
                    {
                        p.GreetingView = p.Greeting3;
                        p.CountryView = p.Country3;
                    }
                    break;
                case 3:
                    foreach (Person p in ((PersonViewModel)DataContext).Persons)
                    {
                        p.GreetingView = p.Greeting4;
                        p.CountryView = p.Country4;
                    }
                    break;
            }

        }

        ///////////////////////               NewCustomerCode                 ///////////////////////////////////////
        //adding new customer
        private void NewCutomerAdd(object sender, RoutedEventArgs e)
        {

            NewCustomerProfileWindow window = new NewCustomerProfileWindow(Countries,Greetings,persons.LanguageId);
            window.CustomerProfileCreated += CreateNewCustomer;

            if (window.ShowDialog() == true)
            {
                MessageBox.Show("End");
            }
            else
            {
                MessageBox.Show("dadad");
            }
        }

        public async void CreateNewCustomer(Person person)
        {


            MessageBox.Show("Good One");
        }


        ///////////////////////                EditingCustomerCode                 ///////////////////////////////////////
        private void Edit_Customer_Click(object sender, RoutedEventArgs e)
        {           
           // int k = languageId + 1;
            int custumerId = persons.SelectedPerson.Id;
            CustomerProfileWindow window = new CustomerProfileWindow(custumerId,1);
            window.CustomerProfileUpdate += EditCustomer;
            window.ShowDialog();
        }

        public async void EditCustomer(Person person)
        {
            string json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("person", data);
            response.EnsureSuccessStatusCode();

            MessageBox.Show("Updated");
        }

        //double click datagrid
        private void personsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           // int k = languageId + 1;
            int custumerId = persons.SelectedPerson.Id;
            CustomerProfileWindow window = new CustomerProfileWindow(custumerId, 1);
            window.CustomerProfileUpdate += EditCustomer;
            window.ShowDialog();
        }

        private void languageSelector_Load(object sender, RoutedEventArgs e)
        {
            switch (persons.LanguageId)
            {
                case 0:
                    foreach (Person p in persons.Persons)
                    {
                        p.GreetingView = p.Greeting1;
                        p.CountryView = p.Country1;
                    }
                    break;
                case 1:
                    foreach (Person p in persons.Persons)
                    {
                        p.GreetingView = p.Greeting2;
                        p.CountryView = p.Country2;
                    }
                    break;
                case 2:
                    foreach (Person p in persons.Persons)
                    {
                        p.GreetingView = p.Greeting3;
                        p.CountryView = p.Country3;
                    }
                    break;
                case 3:
                    foreach (Person p in persons.Persons)
                    {
                        p.GreetingView = p.Greeting4;
                        p.CountryView = p.Country4;
                    }
                    break;
            }
        }
    }
}

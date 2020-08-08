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

        PersonViewModel persons;
        HttpClient client = new HttpClient();

        public MainWindow()
        {

            persons = new PersonViewModel();
            DataContext = persons;
            InitializeComponent();


            client.BaseAddress = new Uri("http://localhost:5000/api/");

            this.Loaded += MainWindow_Loaded;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:5000/");

            //var response = await client.GetAsync("api/data");

            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonResult = await response.Content.ReadAsStringAsync();

            //    var persons1 = JsonConvert.DeserializeObject<IEnumerable<Person>>(jsonResult);

            //    ((PersonViewModel)DataContext).Persons.Clear();

            //    foreach (Person p in persons1)
            //    {
            //        ((PersonViewModel)DataContext).Persons.Add(p);
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            //}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          //  personsGrid.SelectedItems
            int index = personsGrid.SelectedIndex;

            var c = personsGrid.SelectedItems;

            //foreach (Person p in ((PersonViewModel)DataContext).SelectedPersons)
            //{
            //    ((PersonViewModel)DataContext).Persons.Remove(p);
  
            //}

            
        }

        //adding new customer
        private void NewCutomerAdd(object sender, RoutedEventArgs e)
        {
            CustomerProfileWindow window = new CustomerProfileWindow();
            window.ShowDialog();
        }

   
        //mainwindow start
        async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UriBuilder uri = new UriBuilder();
            
            HttpResponseMessage response = await client.GetAsync("/api/data?page=1&text=London");
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var json = await response.Content.ReadAsStringAsync();

            var persons1 = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);


            foreach (Person p in persons1)
            {
                ((PersonViewModel)DataContext).Persons.Add(p);
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        //language change with combobox 
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(languageSelector.SelectedIndex)
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

        //double click datagrid
        private void personsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchBox.Text = ((PersonViewModel)DataContext).SelectedPerson.Lname;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

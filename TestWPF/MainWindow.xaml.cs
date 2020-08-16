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

        public MainWindow()
        {
            persons = new PersonViewModel();
            DataContext = persons;
            InitializeComponent();
        }

        private void NewCutomerAdd_Click(object sender, RoutedEventArgs e)
        {
            NewCustomerProfileWindow window = new NewCustomerProfileWindow(persons.Countries, persons.Greetings, persons.LanguageId);
            window.CustomerProfileCreated += persons.CreateNewCustomer;
            window.ShowDialog();
        }

        private void Edit_Customer_Click(object sender, RoutedEventArgs e)
        {
            CustomerProfileWindow window
                = new CustomerProfileWindow(persons.SelectedPerson.Id,
                persons.LanguageId,
                persons.Countries,
                persons.Greetings);

            window.CustomerProfileUpdated += persons.EditCustomer;
            window.ShowDialog();
        }

        //double click datagrid
        private void personsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {                     
            if(persons.SelectedPerson!=null)
            {
                int custumerId = persons.SelectedPerson.Id;

                CustomerProfileWindow window
               = new CustomerProfileWindow(custumerId, persons.LanguageId, persons.Countries, persons.Greetings);
                window.CustomerProfileUpdated += persons.EditCustomer;
                window.ShowDialog();            
            }
           
        }
    }
}

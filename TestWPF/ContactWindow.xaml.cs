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

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        private HttpClient _client = new HttpClient();

        public ContactWindow()
        {
            InitializeComponent();
            _client.BaseAddress = new Uri("http://localhost:5000/api/");

        }
        public ContactWindow(Contact contact)
        {
            DataContext = contact;
            InitializeComponent();
            ContactTextBox.Text = contact.Txt;
            _client.BaseAddress = new Uri("http://localhost:5000/api/");

        }
    }
}

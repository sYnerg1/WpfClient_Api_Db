using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public Random rand = new Random();
        public event Action<Contact> ContactCreated;

        private ContactVM _contact;

        public NewContactWindow()
        {
            _contact = new ContactVM();
            DataContext = _contact;

            InitializeComponent();
            ContactTypeComboBox.DataContext = _contact.Contact;
        }


        private void NewContact_Save_Click(object sender, RoutedEventArgs e)
        {
            _contact.Contact.ContactTypeId = ContactTypeComboBox.SelectedIndex+1;
            _contact.Contact.PersonContactId = rand.Next();
            ContactCreated(_contact.Contact);
            this.DialogResult = true;
        }

        private void NewContact_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

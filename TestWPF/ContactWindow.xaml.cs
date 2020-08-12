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
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        ContactVM _contactToUpdate;

        public ContactWindow(Contact contact)
        {
            _contactToUpdate = new ContactVM(contact);
            DataContext = _contactToUpdate;
            InitializeComponent();
            ContactTypeComboBox.SelectedIndex = contact.ContactTypeId - 1;
        }

        private void Update_Contact_Click(object sender, RoutedEventArgs e)
        {
            _contactToUpdate.Contact.ContactTypeId = ContactTypeComboBox.SelectedIndex + 1;
            DialogResult = true;
        }

        private void Cancel_Contact_Click(object sender, RoutedEventArgs e)
        {
            _contactToUpdate.Contact.Txt = _contactToUpdate.DefaultTxt;
           DialogResult = false;
        }
    }
}

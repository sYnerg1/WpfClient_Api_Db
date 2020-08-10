using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Models;

namespace TestWPF.ViewModels
{
    public class CustomerProfileVM : INotifyPropertyChanged
    {
        public CustomerProfileVM()
        {
            Contacts = new ObservableCollection<Contact>();
            person = new Person();
        }

        private IList _selectedContacts = new ArrayList();
        public IList SelectedContacts
        {
            get { return _selectedContacts; }
            set
            {
                _selectedContacts = value;
                RaisePropertyChanged("SelectedContacts");
            }
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
        public Contact SelectedContac
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
    }
}

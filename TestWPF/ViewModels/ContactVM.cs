﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Models;

namespace TestWPF.ViewModels
{
    public class ContactVM : INotifyPropertyChanged
    {
        public string DefaultTxt { get; set; }
        public ContactVM()
        {
            contact = new Contact();
        }

        public ContactVM(Contact con)
        {
            contact = con;
            DefaultTxt = con.Txt;
        }

        private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                OnPropertyChanged("Contact");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

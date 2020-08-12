using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestWPF.Models
{
    public class Contact : INotifyPropertyChanged
    {
        public Contact()
        {

        }

        
        private string txt;

        public int PersonContactId { get; set; }

        private int contactTypeId = 0;
        public int ContactTypeId
        {
            get { return contactTypeId; }
            set
            {
                contactTypeId = value;
                OnPropertyChanged("ContactTypeId");
            }
        }

        public string Txt
        {
            get { return txt; }
            set
            {
                txt = value;
                OnPropertyChanged("Txt");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

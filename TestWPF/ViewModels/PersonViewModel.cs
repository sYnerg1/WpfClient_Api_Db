using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Commands;
using TestWPF.Models;

namespace TestWPF.ViewModels
{
    public class PersonViewModel: INotifyPropertyChanged
    {

        private Person selectedPerson;
        private int languageId=0;

        private IList _selectedPersons = new ArrayList();
        public IList SelectedPersons
        {
            get { return _selectedPersons; }
            set
            {
                _selectedPersons = value;
                RaisePropertyChanged("SelectedPersons");
            }
        }

        public ObservableCollection<Person> Persons { get; set; }
        

        public PersonViewModel()
        {
            Persons = new ObservableCollection<Person>();
        }

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }
        public int LanguageId
        {
            get { return languageId; }
            set
            {
                languageId = value;
                OnPropertyChanged("LanguageId");
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

        private RelayCommand seacrhCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return seacrhCommand ??
                  (seacrhCommand = new RelayCommand(Search));
            }
        }

        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return refreshCommand ??
                  (refreshCommand = new RelayCommand(Refresh));
            }
        }


        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(Delete));
            }
        }


        public async void Search(object obj)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/api/");
            Persons.Clear();
            string searchText = obj.ToString();
            HttpResponseMessage response = await client.GetAsync($"/api/person?page=1&text={searchText}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var personsFromApi = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);

            foreach (Person p in personsFromApi)
            {
               Persons.Add(p);
            }
        }


        public async void Refresh(object obj)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/api/");
            Persons.Clear();
            string searchText = obj.ToString();
            HttpResponseMessage response = await client.GetAsync($"/api/person?page=1&text={searchText}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var personsFromApi = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);

            foreach (Person p in personsFromApi)
            {
                Persons.Add(p);
            }
        }


        public async void Delete(object obj)
        {
            var k = obj as IList;
            foreach(Person p in k)
            {
                Persons.Remove(p);
            }
            SelectedPersons.Clear();
        }


    }
}

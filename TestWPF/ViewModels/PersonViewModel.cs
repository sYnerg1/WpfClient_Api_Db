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
using System.Windows.Navigation;
using TestWPF.Commands;
using TestWPF.Models;

namespace TestWPF.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private int page = 1;
        private Person selectedPerson;
        private int languageId = 0;
        HttpClient client;

        public ObservableCollection<Person> Persons { get; set; }

        public PersonViewModel()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/api/");
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
        public int Page
        {
            get { return page; }
            set
            {
                page = value;
                OnPropertyChanged("Page");
                OnPropertyChanged("IsEnableToPreviusPage");
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

        public bool IsEnableToPreviusPage
        {
            get { return page>1; }

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

        private RelayCommand previousPageCommand;
        public RelayCommand PreviousPageCommand
        {
            get
            {
                return previousPageCommand ??
                  (previousPageCommand = new RelayCommand(PreviousPage));
            }
        }

        private RelayCommand nextPageCommand;
        public RelayCommand NextPageCommand
        {
            get
            {
                return nextPageCommand ??
                  (nextPageCommand = new RelayCommand(NextPage));
            }
        }

        public async void Search(object obj)
        {
            Page = 0;
            string searchText = obj.ToString();
            HttpResponseMessage response = await client.GetAsync($"person?page=1&text={searchText}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var personsFromApi = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
            Persons.Clear();
            foreach (Person p in personsFromApi)
            {
                switch (LanguageId)
                {
                    case 0:
                        p.GreetingView = p.Greeting1;
                        p.CountryView = p.Country1;

                        break;
                    case 1:
                        p.GreetingView = p.Greeting2;
                        p.CountryView = p.Country2;
                        break;
                    case 2:
                        p.GreetingView = p.Greeting3;
                        p.CountryView = p.Country3;
                        break;
                    case 3:
                        p.GreetingView = p.Greeting4;
                        p.CountryView = p.Country4;
                        break;
                }
                Persons.Add(p);
            }
        }


        public async void Refresh(object obj)
        {
            Page = 1;
            string searchText = obj.ToString();
            HttpResponseMessage response = await client.GetAsync($"person?page=1&text={searchText}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var personsFromApi = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
            Persons.Clear();
            foreach (Person p in personsFromApi)
            {
                switch (LanguageId)
                {
                    case 0:
                        p.GreetingView = p.Greeting1;
                        p.CountryView = p.Country1;

                        break;
                    case 1:
                        p.GreetingView = p.Greeting2;
                        p.CountryView = p.Country2;
                        break;
                    case 2:
                        p.GreetingView = p.Greeting3;
                        p.CountryView = p.Country3;
                        break;
                    case 3:
                        p.GreetingView = p.Greeting4;
                        p.CountryView = p.Country4;
                        break;
                }
                Persons.Add(p);
            }
        }


        public async void Delete(object obj)
        {

            var castToIList = obj as IList;
            if (obj != null)
            {
                var listOfSelectedItems = castToIList.OfType<Person>().ToList();
                foreach (Person p in listOfSelectedItems)
                {
                    HttpResponseMessage response = await client.DeleteAsync($"person/{p.Id}");
                    response.EnsureSuccessStatusCode();
                    Persons.Remove(SelectedPerson);
                }
            }
        }

        public async void PreviousPage(object obj)
        {
            string searchText = obj.ToString();

            HttpResponseMessage response = await client.GetAsync($"person?page={--Page}&text={searchText}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var personsFromApi = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
            Persons.Clear();
            foreach (Person p in personsFromApi)
            {
                switch (LanguageId)
                {
                    case 0:
                        p.GreetingView = p.Greeting1;
                        p.CountryView = p.Country1;

                        break;
                    case 1:
                        p.GreetingView = p.Greeting2;
                        p.CountryView = p.Country2;
                        break;
                    case 2:
                        p.GreetingView = p.Greeting3;
                        p.CountryView = p.Country3;
                        break;
                    case 3:
                        p.GreetingView = p.Greeting4;
                        p.CountryView = p.Country4;
                        break;
                }
                Persons.Add(p);
            }
        }

        public async void NextPage(object obj)
        {
            string searchText = obj.ToString();

            HttpResponseMessage response = await client.GetAsync($"person?page={++Page}&text={searchText}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var personsFromApi = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
            Persons.Clear();
            foreach (Person p in personsFromApi)
            {
                switch (LanguageId)
                {
                    case 0:
                        p.GreetingView = p.Greeting1;
                        p.CountryView = p.Country1;

                        break;
                    case 1:
                        p.GreetingView = p.Greeting2;
                        p.CountryView = p.Country2;
                        break;
                    case 2:
                        p.GreetingView = p.Greeting3;
                        p.CountryView = p.Country3;
                        break;
                    case 3:
                        p.GreetingView = p.Greeting4;
                        p.CountryView = p.Country4;
                        break;
                }
                Persons.Add(p);
            }
        }
    }
}

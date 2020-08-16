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
using System.Windows;
using System.Windows.Navigation;
using TestWPF.Commands;
using TestWPF.Models;

namespace TestWPF.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public List<Greeting> Greetings;
        public List<Country> Countries;

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

        private RelayCommand languageChangedCommand;
        public RelayCommand LanguageChangedCommand
        {
            get
            {
                return languageChangedCommand ??
                  (languageChangedCommand = new RelayCommand(LanguageChanged));
            }
        }


        private RelayCommand windowLoadedCommand;
        public RelayCommand WindowLoadedCommand
        {
            get
            {
                return windowLoadedCommand ??
                  (windowLoadedCommand = new RelayCommand(WindowLoaded));
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
                Persons.Add(p);
            }
            LanguageChanged(new object());
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
                Persons.Add(p);
            }
            LanguageChanged(new object());
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
                Persons.Add(p);
            }
            LanguageChanged(new object());
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
                Persons.Add(p);
            }
            LanguageChanged(new object());
        }

        public void LanguageChanged(object obj)
        {
            switch (LanguageId)
            {
                case 0:
                    foreach (Person p in Persons)
                    {
                        p.GreetingView = p.Greeting1;
                        p.CountryView = p.Country1;
                    }
                    break;
                case 1:
                    foreach (Person p in Persons)
                    {
                       if(p.Greeting2==null || p.Greeting2=="")
                        {
                            p.GreetingView = p.Greeting1;
                        }
                       else
                        {
                            p.GreetingView = p.Greeting2;
                        }

                       if(p.Country2==null || p.Country2=="")
                        {
                            p.CountryView = p.Country1;
                        }
                       else
                        {
                            p.CountryView = p.Country2;
                        }  
                    }
                    break;
                case 2:
                    foreach (Person p in Persons)
                    {
                        if (p.Greeting3 == null || p.Greeting3 == "")
                        {
                            p.GreetingView = p.Greeting1;
                        }
                        else
                        {
                            p.GreetingView = p.Greeting3;
                        }

                        if (p.Country3 == null || p.Country3 == "")
                        {
                            p.CountryView = p.Country1;
                        }
                        else
                        {
                            p.CountryView = p.Country3;
                        }
                    }
                    break;
                case 3:
                    foreach (Person p in Persons)
                    {
                        if (p.Greeting4 == null || p.Greeting4 == "")
                        {
                            p.GreetingView = p.Greeting1;
                        }
                        else
                        {
                            p.GreetingView = p.Greeting4;
                        }

                        if (p.Country4 == null || p.Country4 == "")
                        {
                            p.CountryView = p.Country1;
                        }
                        else
                        {
                            p.CountryView = p.Country4;
                        }
                    }
                    break;
            }
        }

        public async void WindowLoaded(object obj)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/api/person");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var personsFromApi = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);

                Persons.Clear();

                foreach (Person p in personsFromApi)
                {
                    Persons.Add(p);
                }
                LanguageChanged(new object());

                HttpResponseMessage responseGreeting = await client.GetAsync("/api/greeting");
                responseGreeting.EnsureSuccessStatusCode();
                var jsonGreeting = await responseGreeting.Content.ReadAsStringAsync();

                Greetings = JsonConvert.DeserializeObject<List<Greeting>>(jsonGreeting);

                foreach (Greeting gr in Greetings)
                {
                    if (gr.Txt2 == null || gr.Txt2 == "")
                    {
                        gr.Txt2 = gr.Txt1;
                    }
                    if (gr.Txt3 == null || gr.Txt3 == "")
                    {
                        gr.Txt3 = gr.Txt1;
                    }
                    if (gr.Txt4 == null || gr.Txt4 == "")
                    {
                        gr.Txt4 = gr.Txt1;
                    }
                }

                HttpResponseMessage responseCountry = await client.GetAsync("/api/country");
                responseCountry.EnsureSuccessStatusCode();
                var jsonCountry = await responseCountry.Content.ReadAsStringAsync();

                Countries = JsonConvert.DeserializeObject<List<Country>>(jsonCountry);

                foreach (Country gr in Countries)
                {
                    if (gr.Txt2 == null || gr.Txt2 == "")
                    {
                        gr.Txt2 = gr.Txt1;
                    }
                    if (gr.Txt3 == null || gr.Txt3 == "")
                    {
                        gr.Txt3 = gr.Txt1;
                    }
                    if (gr.Txt4 == null || gr.Txt4 == "")
                    {
                        gr.Txt4 = gr.Txt1;
                    }
                }
            }catch
            {
                MessageBox.Show(" Error!!!!\nMaybe you need change the connection string\n in DataApi project");
            }
        }


        public async void EditCustomer(int id)
        {
            var response = await client.GetAsync($"person/{id}");

            var jsonCustomer = await response.Content.ReadAsStringAsync();

            var customer = JsonConvert.DeserializeObject<Person>(jsonCustomer);
            customer.GreetingView = customer.Greeting1;
            customer.CountryView = customer.Country1;

            var personToUpdate = Persons.FirstOrDefault(x => x.Id == id);

            Persons.Remove(personToUpdate);

            Persons.Add(customer);

        }


        public async void CreateNewCustomer(int id)
        {
            var response = await client.GetAsync($"person/{id}");

            var jsonCustomer = await response.Content.ReadAsStringAsync();

            var customer = JsonConvert.DeserializeObject<Person>(jsonCustomer);
            customer.CountryView = customer.Country1;
            customer.GreetingView = customer.Greeting1;
            Persons.Add(customer);
        }


    }
}

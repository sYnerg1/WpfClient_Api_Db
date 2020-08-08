using AutoMapper;
using DataApi.BAL.DTOs;
using DataApi.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.BAL.Services.Defaults
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _persons;
        private readonly IMapper _map;

        public PersonService(IPersonRepository persons,IMapper map)
        {
            _persons = persons;
            _map = map;
        }

        public async Task<PagedPersonsDTO> Find(SearchFilterDTO filter)
        {
            var queryOfPersons = _persons
                .GetQuery();

            if (filter.SearchText == "" || filter.SearchText == null)
            {

            }
            else
            {
                queryOfPersons = queryOfPersons
                    .Where(x => x.Fname == filter.SearchText ||
                    x.Cpny == filter.SearchText ||
                    x.City == filter.SearchText ||
                    x.Lname == filter.SearchText).Distinct();
                    
            }

            queryOfPersons = queryOfPersons
                .Include(p => p.Greeting)
                .Include(p => p.CountryCodeNavigation)
                .Include(p => p.PersonContact);

            queryOfPersons = queryOfPersons
                .Skip((filter.Page - 1) * 20)
                .Take(20);

            var listOfPersons = await queryOfPersons.ToListAsync();

            PagedPersonsDTO result = new PagedPersonsDTO()
            {
                Persons = _map.Map<IEnumerable<PersonDTO>>(listOfPersons),
                Page = filter.Page
            };

            return result;
        }
    }
}

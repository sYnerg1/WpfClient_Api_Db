using DataApi.DAL.EntityFramework;
using DataApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.DAL.Repositories.Defaults
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DefaultContext _db;
        public PersonRepository(DefaultContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Person value)
        {
            if(value != null)
            {
               await  _db.AddAsync(value);
               await _db.SaveChangesAsync();
            }
            return value.Id;
        }

        public async  Task<bool> DeleteAsync(int id)
        {
            var existPerson = await GetByIdAsync(id);

            if (existPerson != null)
            {
                _db.Remove<Person>(existPerson);
                return (await _db.SaveChangesAsync()) > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var person = await _db.Person
                .Include(p=>p.PersonContact)
                .Include(p=>p.CountryCodeNavigation)
                .Include(p=>p.Greeting)
                .FirstOrDefaultAsync(x=>x.Id==id);

            return person;
        }

        public IQueryable<Person> GetQuery()
        {
            return _db.Person.AsQueryable();
        }

        public async Task<bool> UpdateAsync(int id, Person person)
        {
            var existPerson = await _db.Person
                .Include(p => p.PersonContact)
                .Include(p => p.CountryCodeNavigation)
                .Include(p => p.Greeting)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existPerson!=null)
            {
                _db.Entry(existPerson).CurrentValues.SetValues(person);

                existPerson.PersonContact = person.PersonContact;

                return (await _db.SaveChangesAsync())>0;
            }
            else
            {
                return false;
            }
        }
    }
}

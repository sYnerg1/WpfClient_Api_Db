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

        public async Task AddAsync(Person value)
        {
            if(value != null)
            {
               await  _db.AddAsync(value);
               await _db.SaveChangesAsync();
            }
        }

        public Task AddRangeAsync(IEnumerable<Person> value)
        {
            throw new NotImplementedException();
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
    }
}

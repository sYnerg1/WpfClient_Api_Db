using DataApi.DAL.EntityFramework;
using DataApi.DAL.Models;
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

        public Task<Person> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Person> GetQuery()
        {
            return _db.Person.AsQueryable();
        }
    }
}

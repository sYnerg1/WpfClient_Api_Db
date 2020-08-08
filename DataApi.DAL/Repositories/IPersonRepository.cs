using DataApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.DAL.Repositories
{
    public interface IPersonRepository
    {
        Task AddAsync(Person value);
        Task AddRangeAsync(IEnumerable<Person> value);
        Task<Person> GetByIdAsync(int id);
        IQueryable<Person> GetQuery();
    }
}

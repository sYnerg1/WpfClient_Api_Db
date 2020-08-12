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
        Task<int> AddAsync(Person value);
        Task<Person> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id,Person person);
        Task<bool> DeleteAsync(int id);
        IQueryable<Person> GetQuery();
    }
}

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
    public class CountryRepository : ICountryRepository
    {
        private readonly DefaultContext _db;

        public CountryRepository(DefaultContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var countries = await _db.Country.ToListAsync();

            return countries;
        }
    }
}

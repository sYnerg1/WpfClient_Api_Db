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
    public class GreetingRepository:IGreetingRepository
    {
        private readonly DefaultContext _db;
        public GreetingRepository(DefaultContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Greeting>> GetAllGreetingAsync()
        {
            var greetings = await _db.Greeting.ToListAsync();
            return greetings;
        }
    }
}

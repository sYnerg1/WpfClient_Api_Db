using DataApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.DAL.Repositories
{
    public interface IGreetingRepository
    {
        Task<IEnumerable<Greeting>> GetAllGreetingAsync();
    }
}

using DataApi.BAL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.BAL.Services
{
    public interface IGreetingService
    {
        Task<IEnumerable<GreetingDTO>> GetAllGreetingAsync();
    }
}

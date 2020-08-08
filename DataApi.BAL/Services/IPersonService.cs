using DataApi.BAL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.BAL.Services
{
    public interface IPersonService
    {
        Task<PagedPersonsDTO> Find(SearchFilterDTO filter);
    }
}

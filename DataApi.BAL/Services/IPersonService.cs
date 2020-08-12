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
        Task<PersonDTO> GetByIdAsync(int id);
        Task<int> AddAsync(PersonDTO personDTO);
        Task<bool> UpdateAsync(int id,PersonDTO personDTO);
        Task<bool> DeleteAsync(int id);
    }
}

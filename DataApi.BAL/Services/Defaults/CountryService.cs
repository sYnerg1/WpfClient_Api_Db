using AutoMapper;
using DataApi.BAL.DTOs;
using DataApi.DAL.Models;
using DataApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.BAL.Services.Defaults
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repo;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
    }
        public async Task<IEnumerable<CountryDTO>> GetAllCountriesAsync()
        {
            var countries = await _repo.GetAllAsync();
            var countriesDTO = _mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(countries);
            return countriesDTO;

        }
    }
}

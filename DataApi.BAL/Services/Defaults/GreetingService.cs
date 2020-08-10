using AutoMapper;
using DataApi.BAL.DTOs;
using DataApi.DAL.Models;
using DataApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.BAL.Services.Defaults
{
    class GreetingService : IGreetingService
    {
        private readonly IGreetingRepository _repo;
        private readonly IMapper _mapper;

        public GreetingService(IGreetingRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GreetingDTO>> GetAllGreetingAsync()
        {
            var greetings = await _repo.GetAllGreetingAsync();

            var greetingsDTO = _mapper.Map<IEnumerable<Greeting>, IEnumerable<GreetingDTO> >(greetings);

            return greetingsDTO;
        }
    }
}

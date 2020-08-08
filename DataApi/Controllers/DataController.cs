using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataApi.BAL.DTOs;
using DataApi.BAL.Services;
using DataApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

      public DataController(IMapper mapper, IPersonService personService)
        {
            _mapper = mapper;
            _personService = personService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery]int page,string text)
        {
            
            SearchFilterDTO filter = new SearchFilterDTO()
            { 
                Page = page,
              SearchText =text
            };

            var result = await _personService.Find(filter);
            return Ok(result.Persons);
        }


       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            
                return Ok();
            
        }

        // POST api/<DataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

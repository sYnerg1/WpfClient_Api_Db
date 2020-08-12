using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataApi.BAL.DTOs;
using DataApi.BAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public PersonController(IMapper mapper, IPersonService personService)
        {
            _mapper = mapper;
            _personService = personService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] string text, int page = 1)
        {

            SearchFilterDTO filter = new SearchFilterDTO()
            {
                Page = page,
                SearchText = text
            };

            var result = await _personService.Find(filter);
            return Ok(result.Persons);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var person = await _personService.GetByIdAsync(id);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonDTO person)
        {
            int createdPersonId = await _personService.AddAsync(person);
            return StatusCode(201, createdPersonId);
        }

        // PUT api/<DataController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PersonDTO person)
        {
            var result = await _personService.UpdateAsync(id,person);

            if (result)
            {
                return Ok(id);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<DataController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           var result = await _personService.DeleteAsync(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

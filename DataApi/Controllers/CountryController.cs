using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataApi.BAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countrtService)
        {
            _countryService = countrtService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try {
                var result = await _countryService.GetAllCountriesAsync();

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }      
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataApi.BAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        private readonly IGreetingService _greetingService;

        public GreetingController(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _greetingService.GetAllGreetingAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
            
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeeloCore.Entities;
using WeeloCore.Logic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeeloAPI.Controllers
{
    //In this class all the services associated with the country are consumed
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper mapper;
        private CountryLogic countryLogic;

        //Controller
        public CountryController(IMapper mapper)
        {
            this.mapper = mapper;
            countryLogic = new CountryLogic(mapper);
        }

        // GET: api/<CountryController>
        //Method to get all system countries
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get()
        {
            var countries = new List<CountryEntity>();
            await Task.Run(() =>
            {
                countries = countryLogic.GetAll();
            });

            if (countries.Any()) return Ok(countries);
            return NotFound(countries);
        }

    }
}

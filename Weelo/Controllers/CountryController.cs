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
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper mapper;
        private CountryLogic countryLogic;

        public CountryController(IMapper mapper)
        {
            this.mapper = mapper;
            countryLogic = new CountryLogic(mapper);
        }

        // GET: api/<CountryController>
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
            return NotFound();
        }

    }
}

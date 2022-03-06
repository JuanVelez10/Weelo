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
    //In this class all the services associated with the city are consumed
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMapper mapper;
        private CityLogic cityLogic;

        //Controller
        public CityController(IMapper mapper)
        {
            this.mapper = mapper;
            cityLogic = new CityLogic(mapper);
        }

        // GET: api/<CityController>
        //Method to get all system cities
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get()
        {
            var cities = new List<CityEntity>();
            await Task.Run(() =>
            {
                cities = cityLogic.GetAll();
            });

            if (cities.Any()) return Ok(cities);
            return NotFound();
        }

    }
}

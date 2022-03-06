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
    //In this class all the services associated with the zone are consumed
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IMapper mapper;
        private ZoneLogic zoneLogic;

        //Controller
        public ZoneController(IMapper mapper)
        {
            this.mapper = mapper;
            zoneLogic = new ZoneLogic(mapper);
        }

        // GET: api/<ZoneController>
        //Method to get all system zones
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get()
        {
            var zones = new List<ZoneEntity>();
            await Task.Run(() =>
            {
                zones = zoneLogic.GetAll();
            });

            if (zones.Any()) return Ok(zones);
            return NotFound();
        }

    }
}

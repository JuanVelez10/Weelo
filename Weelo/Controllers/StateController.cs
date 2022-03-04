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
    public class StateController : ControllerBase
    {
        private readonly IMapper mapper;
        private StateLogic stateLogic;

        public StateController(IMapper mapper)
        {
            this.mapper = mapper;
            stateLogic = new StateLogic(mapper);
        }

        // GET: api/<StateController>
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get()
        {
            var states = new List<StateEntity>();
            await Task.Run(() =>
            {
                states = stateLogic.GetAll();
            });

            if (states.Any()) return Ok(states);
            return NotFound();
        }

    }
}

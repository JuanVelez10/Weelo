using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeeloAPI.References;
using WeeloCore.Entities;
using WeeloCore.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeeloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PropertyTraceController : ControllerBase
    {
        private readonly IMapper mapper;
        private PropertyTraceLogic propertyTraceLogic;

        public PropertyTraceController(IMapper mapper)
        {
            this.mapper = mapper;
            propertyTraceLogic = new PropertyTraceLogic(mapper);
        }

        // GET: api/<PropertyTraceController>/Property/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to obtain all the traces of a property
        [HttpGet("Property/{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var propertyTraces = new List<PropertyTraceEntity>();
            await Task.Run(() =>
            {
                propertyTraces = propertyTraceLogic.GetAllForProperty(id);
            });

            if (propertyTraces.Any()) return Ok(propertyTraces);
            return NotFound();
        }

        // POST api/<PropertyTraceController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] PropertyTraceRequest propertyTraceRequest)
        {
            var responseLogic = propertyTraceLogic.Insert(mapper.Map<PropertyTraceEntity>(propertyTraceRequest));
            if (responseLogic != null) return Ok(responseLogic);
            return BadRequest();
        }

    }
}

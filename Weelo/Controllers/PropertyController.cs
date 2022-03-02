using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeeloAPI.Helpers;
using WeeloAPI.References;
using WeeloCore.Entities;
using WeeloCore.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeeloAPI.Controllers
{
    //In this class, all the services associated with the properties or dwellings are consumed.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PropertyController : ControllerBase
    {
        private readonly IMapper mapper;
        private PropertyLogic propertyLogic;
        private ToolsConfig toolsConfig;

        public PropertyController(IMapper mapper)
        {
            this.mapper = mapper;
            this.propertyLogic = new PropertyLogic(mapper);
            toolsConfig = new ToolsConfig();
        }

        // GET api/<PropertyController>
        //Method to get all system properties
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties = propertyLogic.GetAll();
            if (properties.Any()) return Ok(properties);
            return NotFound();
        }

        // GET api/<PropertyController>/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to get a specific property,with detailed information
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public IActionResult Get(Guid? Id)
        {
            var property = propertyLogic.Get(Id);
            if (property != null) return Ok(property);
            return NotFound();
        }

        // POST api/<PropertyController>/Find
        //Method to search for properties by city, area, price, year, room number among other filters
        [HttpPost]
        [Route("Find")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Find([FromBody] FindPropertyRequest findPropertyRequest)
        {
            var properties = propertyLogic.Find(mapper.Map<FindPropertyEntity>(findPropertyRequest));
            if (properties.Any()) return Ok(properties);
            return NotFound();
        }

        // POST api/<PropertyController>
        //Method to add a new property
        [HttpPost]
        public IActionResult Post([FromBody] PropertyRequest propertyRequest)
        {
            var response = propertyLogic.Insert(mapper.Map<PropertyEntity>(propertyRequest));
            if (response != null) return Ok(response);
            return NotFound();
        }

        // PUT api/<PropertyController>
        //Method to update a property
        [HttpPut()]
        public IActionResult Put(int id, [FromBody] PropertyRequest propertyRequest)
        {
            var response = propertyLogic.Update(mapper.Map<PropertyEntity>(propertyRequest));
            if (response != null) return Ok(response);
            return NotFound();
        }

        // DELETE api/<PropertyController>
        //Method to delete a property
        [HttpDelete()]
        public IActionResult Delete(Guid? id)
        {
            var response = propertyLogic.Delete(id);
            if (response != null) return Ok(response);
            return NotFound();
        }
    }
}

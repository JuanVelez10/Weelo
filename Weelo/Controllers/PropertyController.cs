using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    //In this class, all the services associated with the properties or dwellings are consumed.
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private PropertyLogic propertyLogic;

        //Controller
        public PropertyController(IMapper mapper, IConfiguration iConfig)
        {
            this.mapper = mapper;
            config = iConfig;
            propertyLogic = new PropertyLogic(mapper);
        }

        // GET api/<PropertyController>
        //Method to get all system properties
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var properties = new List<PropertyEntity>();
            await Task.Run(() =>
            {
                properties = propertyLogic.GetAll();
            });

            if (properties.Any()) return Ok(properties);
            return NotFound(properties);
        }

        // GET api/<PropertyController>/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to get a specific property,with detailed information
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public IActionResult Get(Guid? id)
        {
            var property = propertyLogic.Get(id);
            if (property != null) return Ok(property);
            return NotFound(property);
        }

        // POST api/<PropertyController>/Find
        //Method to search for properties by city, area, price, year, room number among other filters
        [HttpPost]
        [Route("Find")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Find([FromBody] FindPropertyRequest findPropertyRequest)
        {
            var properties = new List<PropertyBasicEntity>();

            await Task.Run(() =>
            {
                var itemsForPage = Convert.ToInt32( config.GetSection("Page")["ItemsForPage"]);
                properties = propertyLogic.Find(mapper.Map<FindPropertyEntity>(findPropertyRequest), itemsForPage);
            });

            if (properties.Any()) return Ok(properties);
            return BadRequest(properties);
        }

        // POST api/<PropertyController>
        //Method to add a new property
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] PropertyRequest propertyRequest)
        {
            var response = propertyLogic.Insert(mapper.Map<PropertyEntity>(propertyRequest));
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // PUT api/<PropertyController>
        //Method to update a property
        [HttpPut()]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] PropertyRequest propertyRequest)
        {
            var response = propertyLogic.Update(mapper.Map<PropertyEntity>(propertyRequest));
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // DELETE api/<PropertyController>/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to delete a property
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            var response = propertyLogic.Delete(id);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // PATCH api/<PropertyController>/Enable
        //Method to delete a property
        [HttpPatch()]
        [Route("Enable")]
        [Authorize(Roles = "Admin")]
        public IActionResult Enable(Guid? id, bool enable)
        {
            var response = propertyLogic.UpdatePropertyEnable(id, enable);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // PATCH api/<PropertyController>/Price
        //Method to delete a property
        [HttpPatch()]
        [Route("Price")]
        [Authorize(Roles = "Admin")]
        public IActionResult Price(Guid? id, decimal price)
        {
            var response = propertyLogic.UpdatePropertyPrice(id, price);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeeloAPI.Helpers;
using WeeloAPI.References;
using WeeloCore.Entities;
using WeeloCore.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeeloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PropertyImageController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private PropertyImageLogic propertyImageLogic;
        private PropertyLogic propertyLogic;
        private ToolsConfig toolsConfig;

        public PropertyImageController(IMapper mapper, IConfiguration iConfig)
        {
            this.mapper = mapper;
            config = iConfig;
            propertyImageLogic = new PropertyImageLogic(mapper);
            propertyLogic = new PropertyLogic(mapper);
            toolsConfig = new ToolsConfig();
        }

        //GET: api/<PropertyImageController>/Property/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to obtain all the images of a property
        [HttpGet("Property/{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var propertyImages = new List<PropertyImageBasicEntity>();
            await Task.Run(() =>
            {
                propertyImages = propertyImageLogic.GetAllForProperty(id);
            });

            if (propertyImages.Any()) return Ok(propertyImages);
            return NotFound();
        }

        //POST api/<PropertyImageController>
        //Method to add a image of property
        [HttpPost]
        [Route("")]
        public IActionResult Post()
        {
            if (Request.Form == null) return BadRequest();
            if (!Request.Form.Files.Any()) return BadRequest();
            if (!Request.Form.Keys.Any()) return BadRequest();

            var urlImage = string.Empty;
            var idProperty = string.Empty;

            if (!Request.Form.Where(x => x.Key == "id").Any()) return BadRequest();
            idProperty = Request.Form.Where(x => x.Key == "id").FirstOrDefault().Value;
            if (string.IsNullOrEmpty(idProperty)) return BadRequest();

            Guid guidProperty;
            if (Guid.TryParse(idProperty, out guidProperty))
            {
                var property = propertyLogic.Get(guidProperty);
                if (property == null) NotFound();

                if (!Request.Form.Files.Where(x => x.Name == "image" && x.Length > 0).Any()) return BadRequest();
                var file = Request.Form.Files.Where(x => x.Name == "image" && x.Length > 0).FirstOrDefault();
                urlImage = toolsConfig.UpLoadImage(file.OpenReadStream(), file.FileName, config).Result;
                if (string.IsNullOrEmpty(urlImage)) return BadRequest();

                var response = propertyImageLogic.Insert(new PropertyImageEntity(urlImage, property.Id));
                if (response != null) return Ok(response);
            }

            return BadRequest();
        }

        // PATCH api/<PropertyController>/Enable
        //Method to enable a image of property
        [HttpPatch()]
        [Route("Enable")]
        public IActionResult Enable(Guid? id, bool enable)
        {
            //var response = propertyImageLogic.UpdatePropertyEnable(id, enable);
            //if (response != null) return Ok(response);
            return BadRequest();
        }

    }
}

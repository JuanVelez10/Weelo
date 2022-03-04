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
using static WeeloCore.Helpers.EnumType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeeloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Post()
        {
            var response = toolsConfig.ValidateRequest(Request);
            if(response.Code > 0) return BadRequest(response);

            var idProperty = Request.Form.Where(x => x.Key == "id").FirstOrDefault().Value;
            var file = Request.Form.Files.Where(x => x.Name == "image" && x.Length > 0).FirstOrDefault();

            response = toolsConfig.ValidateGuid(idProperty);
            if (response.Code > 0) return BadRequest(response);

            response = toolsConfig.ValidateFile(file.FileName);
            if (response.Code > 0) return BadRequest(response);

            var urlImage = toolsConfig.UpLoadImage(file.OpenReadStream(), file.FileName, config).Result;

            var propertyGuid = Guid.Parse(idProperty);
            var property = propertyLogic.Get(propertyGuid);
            if (property == null) return BadRequest(toolsConfig.MessageResponse(3, MessageType.Error, "Property"));

            var responseLogic = propertyImageLogic.Insert(new PropertyImageEntity(urlImage, propertyGuid));
            if (responseLogic != null) return Ok(responseLogic);
            
            return BadRequest();
        }

        //PATCH api/<PropertyImageController>/Enable
        //Method to enable a image of property
        [HttpPatch()]
        [Route("Enable")]
        [Authorize(Roles = "Admin")]
        public IActionResult Enable(Guid? id, bool enable)
        {
            var response = propertyImageLogic.UpdatePropertyImageEnable(id, enable);
            if (response != null) return Ok(response);
            return BadRequest();
        }

    }
}

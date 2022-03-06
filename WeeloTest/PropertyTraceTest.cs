using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using WeeloAPI.Controllers;
using WeeloAPI.Helpers;
using WeeloCore.Entities;


namespace WeeloTest
{
    public class PropertyTraceTest
    {
        private PropertyTraceController propertyTraceController;
        private PropertyController propertyController;

        [SetUp]
        public void Init()
        {
            var mapper = GetIMapper();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            propertyTraceController = new PropertyTraceController(mapper);
            propertyController = new PropertyController(mapper, config);
        }

        private IMapper GetIMapper()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }

        //Test: If the GetAll service brings an existing images of property, validate that it responds correctly
        [Test]
        public async Task GetSucces()
        {
            var responseGetAll = await propertyController.GetAll() as OkObjectResult;
            var properties = responseGetAll.Value as List<PropertyEntity>;

            foreach (var property in properties)
            {
                var responseGet  = propertyController.Get(property.Id) as OkObjectResult;
                var propertyInfo = responseGet.Value as PropertyEntity;
                if (propertyInfo.PropertyTraces.Any())
                {
                    var reponseGet = await propertyTraceController.Get(propertyInfo.Id) as OkObjectResult;
                    Assert.IsNotNull(reponseGet);
                    Assert.AreEqual(200, reponseGet.StatusCode);
                    var propertyTraceOutput = reponseGet.Value as List<PropertyTraceEntity>;
                    Assert.IsTrue(propertyTraceOutput.Any());
                    propertyTraceOutput.ForEach(x => Assert.IsNotNull(x));
                    propertyTraceOutput.ForEach(x => Assert.IsNotNull(x.Name));

                    break;
                }

            }

        }

        //Test: If the GetAll service does not bring images of property if it is sent an id that does not exist
        [Test]
        public async Task GetError()
        {
            var reponseNotFound = await propertyTraceController.Get(Guid.NewGuid()) as NotFoundObjectResult;
            Assert.IsNotNull(reponseNotFound);
            Assert.AreEqual(404, reponseNotFound.StatusCode);
        }

    }
}

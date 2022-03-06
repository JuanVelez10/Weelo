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
using WeeloAPI.References;
using WeeloCore.Entities;
using static WeeloCore.Helpers.EnumType;

namespace WeeloTest
{
    public class PropertyTest
    {
        private PropertyController propertyController;
        private CityController cityController;

        [SetUp]
        public void Init()
        {
            var mapper = GetIMapper();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            propertyController = new PropertyController(mapper, config);
            cityController = new CityController(mapper);
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

        //Test: If the GetAll service brings properties, validate that it works well
        [Test]
        public async Task GetAllSucces()
        {
            Assert.IsTrue(propertyController.GetAll().Wait(60000));
            var reponse = await propertyController.GetAll() as OkObjectResult;
            Assert.IsNotNull(reponse);
            Assert.AreEqual(200, reponse.StatusCode);
            var properties = reponse.Value as List<PropertyEntity>;
            Assert.IsTrue(properties.Any());
            properties.ForEach(x => Assert.IsNotNull(x));
            properties.ForEach(x => Assert.IsNotNull(x.Name));
        }

        //Test: If the GetAll service does not bring properties or generate an error, validate that it responds correctly
        [Test]
        public void GetAllError()
        {
            Assert.IsFalse(propertyController.GetAll().Wait(0));
        }

        //Test: If the Get service brings an existing property, validate that it responds correctly
        [Test]
        public async Task GetSucces()
        {
            var responseGetAll = await propertyController.GetAll() as OkObjectResult;
            var properties = responseGetAll.Value as List<PropertyEntity>;
            var propertyInput = properties.FirstOrDefault();
            var reponseGet = propertyController.Get(propertyInput.Id) as OkObjectResult;
            Assert.IsNotNull(reponseGet);
            Assert.AreEqual(200, reponseGet.StatusCode);
            var propertyOutput = reponseGet.Value as PropertyEntity;
            Assert.IsNotNull(propertyOutput);
            Assert.AreEqual(propertyInput.Id, propertyOutput.Id);
            Assert.AreEqual(propertyInput.Name, propertyOutput.Name);
            Assert.AreEqual(propertyInput.Price, propertyOutput.Price);
            Assert.AreEqual(propertyInput.Year, propertyOutput.Year);
        }

        //Test: If the Get service does not bring property if it is sent an id that does not exist
        [Test]
        public void GetError()
        {
            var reponseNotFound = propertyController.Get(Guid.NewGuid()) as NotFoundObjectResult;
            Assert.IsNotNull(reponseNotFound);
            Assert.AreEqual(404, reponseNotFound.StatusCode);
        }

        //Test: To search for properties by a city that exists
        [Test]
        public async Task FindSucces()
        {
            var reponseCities = await cityController.Get() as OkObjectResult;
            var cities = reponseCities.Value as List<CityEntity>;
            var city = cities.FirstOrDefault();

            FindPropertyRequest findPropertyRequest = new FindPropertyRequest(city.Id);

            var reponseProperties = await propertyController.Find(findPropertyRequest) as OkObjectResult;
            Assert.IsNotNull(reponseProperties);
            Assert.AreEqual(200, reponseProperties.StatusCode);
            var properties = reponseProperties.Value as List<PropertyBasicEntity>;
            Assert.IsTrue(properties.Any());
            properties.ForEach(x => Assert.IsNotNull(x));
            properties.ForEach(x => Assert.IsNotNull(x.Name));
        }

        //Test: To search for properties by a city that does not exist
        [Test]
        public async Task FindError()
        {
            FindPropertyRequest findPropertyRequest = new FindPropertyRequest(Guid.NewGuid());
            var reponseBadRequest = await propertyController.Find(findPropertyRequest) as BadRequestObjectResult;
            Assert.IsNotNull(reponseBadRequest);
            Assert.AreEqual(400, reponseBadRequest.StatusCode);

        }

        //Test: To validate that a property exists to be able to add, update or delete
        [Test]
        public async Task ValidateExist()
        {
            var reponseProperty = await propertyController.GetAll() as OkObjectResult;
            var properties = reponseProperty.Value as List<PropertyEntity>;
            var property = properties.FirstOrDefault();

            var propertyRequest = GetIMapper().Map<PropertyRequest>(property);

            var reponseProperties = propertyController.Validate(propertyRequest,true) as OkObjectResult;
            Assert.IsNotNull(reponseProperties);
            Assert.AreEqual(200, reponseProperties.StatusCode);
            var propertyOutput = reponseProperties.Value as BaseResponse<PropertyEntity>;
            Assert.IsNotNull(propertyOutput);
            Assert.AreEqual(MessageType.Error, propertyOutput.MessageType);
            Assert.AreEqual(7, propertyOutput.Code);  

        }

        //Test: To validate that a property not exists to be able to add, update or delete
        [Test]
        public async Task ValidateNoExist()
        {
            var reponseProperty = await propertyController.GetAll() as OkObjectResult;
            var properties = reponseProperty.Value as List<PropertyEntity>;
            var property = properties.FirstOrDefault();

            var propertyRequest = GetIMapper().Map<PropertyRequest>(property);
            propertyRequest.Id = new Guid();
            propertyRequest.Address = Guid.NewGuid().ToString();

            var reponseProperties = propertyController.Validate(propertyRequest, true) as OkObjectResult;
            Assert.IsNotNull(reponseProperties);
            Assert.AreEqual(200, reponseProperties.StatusCode);
            var propertyOutput = reponseProperties.Value as BaseResponse<PropertyEntity>;
            Assert.IsNotNull(propertyOutput);
            Assert.AreEqual(MessageType.None, propertyOutput.MessageType);
            Assert.AreEqual(0, propertyOutput.Code);

        }


    }
}

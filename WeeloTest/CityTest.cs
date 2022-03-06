using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WeeloAPI.Controllers;
using WeeloAPI.Helpers;
using WeeloCore.Entities;

namespace WeeloTest
{
    public class CityTest
    {
        private CityController cityController;

        [SetUp]
        public void Init()
        {
            var mapper = GetIMapper();
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

        //Test: If the GetAll service brings cities, validate that it works well
        [Test]
        public async Task GetAllSucces()
        {
            Assert.IsTrue(cityController.Get().Wait(60000));
            var reponse = await cityController.Get() as OkObjectResult;
            Assert.IsNotNull(reponse);
            Assert.AreEqual(200, reponse.StatusCode);
            var cities = reponse.Value as List<CityEntity>;
            Assert.IsTrue(cities.Any());
            cities.ForEach(x => Assert.IsNotNull(x));
            cities.ForEach(x => Assert.IsNotNull(x.Name));
        }

        //Test: If the GetAll service does not bring cities or generate an error, validate that it responds correctly
        [Test]
        public void GetAllError()
        {
            Assert.IsFalse(cityController.Get().Wait(0));
        }

    }
}

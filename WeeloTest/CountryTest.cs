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
    public class CountryTest
    {
        private CountryController countryController;

        [SetUp]
        public void Init()
        {
            var mapper = GetIMapper();
            countryController = new CountryController(mapper);
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

        //Test: If the GetAll service brings countries, validate that it works well
        [Test]
        public async Task GetAllSucces()
        {
            Assert.IsTrue(countryController.Get().Wait(60000));
            var reponse = await countryController.Get() as OkObjectResult;
            Assert.IsNotNull(reponse);
            Assert.AreEqual(200, reponse.StatusCode);
            var countries = reponse.Value as List<CountryEntity>;
            Assert.IsTrue(countries.Any());
            countries.ForEach(x => Assert.IsNotNull(x));
            countries.ForEach(x => Assert.IsNotNull(x.Name));
        }

        //Test: If the GetAll service does not bring countries or generate an error, validate that it responds correctly
        [Test]
        public void GetAllError()
        {
            Assert.IsFalse(countryController.Get().Wait(0));
        }
    }
}

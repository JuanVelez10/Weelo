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
    public class ZoneTest
    {
        private ZoneController zoneController;

        [SetUp]
        public void Init()
        {
            var mapper = GetIMapper();
            zoneController = new ZoneController(mapper);
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

        //Test: If the GetAll service brings zones, validate that it works well
        [Test]
        public async Task GetAllSucces()
        {
            Assert.IsTrue(zoneController.Get().Wait(60000));
            var reponse = await zoneController.Get() as OkObjectResult;
            Assert.IsNotNull(reponse);
            Assert.AreEqual(200, reponse.StatusCode);
            var zones = reponse.Value as List<ZoneEntity>;
            Assert.IsTrue(zones.Any());
            zones.ForEach(x => Assert.IsNotNull(x));
            zones.ForEach(x => Assert.IsNotNull(x.Name));
        }

        //Test: If the GetAll service does not bring zones or generate an error, validate that it responds correctly
        [Test]
        public void GetAllError()
        {
            Assert.IsFalse(zoneController.Get().Wait(0));
        }

    }
}

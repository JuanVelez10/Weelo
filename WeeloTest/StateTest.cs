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
    public class StateTest
    {
        private StateController stateController;

        [SetUp]
        public void Init()
        {
            var mapper = GetIMapper();
            stateController = new StateController(mapper);
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

        //Test: If the GetAll service brings states, validate that it works well
        [Test]
        public async Task GetAllSucces()
        {
            Assert.IsTrue(stateController.Get().Wait(60000));
            var reponse = await stateController.Get() as OkObjectResult;
            Assert.IsNotNull(reponse);
            Assert.AreEqual(200, reponse.StatusCode);
            var states = reponse.Value as List<StateEntity>;
            Assert.IsTrue(states.Any());
            states.ForEach(x => Assert.IsNotNull(x));
            states.ForEach(x => Assert.IsNotNull(x.Name));
        }

        //Test: If the GetAll service does not bring states or generate an error, validate that it responds correctly
        [Test]
        public void GetAllError()
        {
            Assert.IsFalse(stateController.Get().Wait(0));
        }

    }
}

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

namespace WeeloTest
{
    public class AccountTest
    {
        private AccountController accountController;

        [SetUp]
        public void Init()
        {
            var mapper = GetIMapper();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            accountController = new AccountController(mapper, config);
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

        //Test: If the GetAll service brings accounts, validate that it works well
        [Test]
        public async Task GetAllSucces()
        {
            Assert.IsTrue(accountController.GetAll().Wait(60000));
            var reponse = await accountController.GetAll() as OkObjectResult;
            Assert.IsNotNull(reponse);
            Assert.AreEqual(200, reponse.StatusCode);
            var accounts = reponse.Value as List<AccountEntity>;
            Assert.IsTrue(accounts.Any());
            accounts.ForEach(x => Assert.IsNotNull(x));
            accounts.ForEach(x => Assert.IsNotNull(x.Name));
        }

        //Test: If the GetAll service does not bring accounts or generate an error, validate that it responds correctly
        [Test]
        public void GetAllError()
        {
            Assert.IsFalse(accountController.GetAll().Wait(0));
        }

        //Test: If the Get service brings an existing account, validate that it responds correctly
        [Test]
        public async Task GetSucces()
        {
            var responseGetAll = await accountController.GetAll() as OkObjectResult;
            var accounts = responseGetAll.Value as List<AccountEntity>;
            var accountInput = accounts.FirstOrDefault();
            var reponseGet = accountController.Get(accountInput.Id) as OkObjectResult;
            Assert.IsNotNull(reponseGet);
            Assert.AreEqual(200, reponseGet.StatusCode);
            var accountOutput = reponseGet.Value as AccountEntity;
            Assert.IsNotNull(accountOutput);
            Assert.AreEqual(accountInput.Id, accountOutput.Id);
            Assert.AreEqual(accountInput.Name, accountOutput.Name);
            Assert.AreEqual(accountInput.Email, accountOutput.Email);
            Assert.AreEqual(accountInput.Password, accountOutput.Password);
        }

        //Test: If the Get service does not bring accounts if it is sent an id that does not exist
        [Test]
        public void GetError()
        {
            var reponseNotFound = accountController.Get(Guid.NewGuid()) as NotFoundObjectResult;
            Assert.IsNotNull(reponseNotFound);
            Assert.AreEqual(404, reponseNotFound.StatusCode);
        }

        //Test: If the Login service works perfectly with an existing account
        [Test]
        public async Task LoginSucces()
        {
            var responseGetAll = await accountController.GetAll() as OkObjectResult;
            var accounts = responseGetAll.Value as List<AccountEntity>;
            var accountInput = accounts.FirstOrDefault();

            LoginRequest login = new LoginRequest(accountInput.Email, accountInput.Password);
            var responseLogin = accountController.Login(login) as OkObjectResult;
            Assert.IsNotNull(responseLogin);
            Assert.AreEqual(200, responseLogin.StatusCode);
            var loginOutput = responseLogin.Value as BaseResponse<AccountEntity>;
            Assert.IsNotNull(loginOutput);
            Assert.AreEqual(1, loginOutput.Code);
            Assert.AreEqual(accountInput.Id, loginOutput.Data.Id);
            Assert.AreEqual(accountInput.Name, loginOutput.Data.Name);
            Assert.AreEqual(accountInput.Email, loginOutput.Data.Email);
            Assert.AreEqual(accountInput.Password, loginOutput.Data.Password);
        }

        //Test: If the login service works perfectly with an account that does not exist
        [Test]
        public void LoginError()
        {
            LoginRequest login = new LoginRequest("test@gmail.com", "1234");
            var reponseBadRequest = accountController.Login(login) as BadRequestObjectResult;
            Assert.IsNotNull(reponseBadRequest);
            Assert.AreEqual(400, reponseBadRequest.StatusCode);
        }

        //Test: If the logged service works perfectly with an existing token
        [Test]
        public async Task LoggedSucces()
        {
            var responseGetAll = await accountController.GetAll() as OkObjectResult;
            var accounts = responseGetAll.Value as List<AccountEntity>;
            var accountInput = accounts.FirstOrDefault();

            LoginRequest login = new LoginRequest(accountInput.Email, accountInput.Password);
            var responseLogin = accountController.Login(login) as OkObjectResult;
            var loginOutput = responseLogin.Value as BaseResponse<AccountEntity>;

            var responseLogged = accountController.Logged(loginOutput.Data.Token) as OkObjectResult;
            Assert.IsNotNull(responseLogged);
            Assert.AreEqual(200, responseLogged.StatusCode);
            var loggedOutput = responseLogged.Value as AccountEntity;

            Assert.IsNotNull(loggedOutput);
            Assert.AreEqual(loginOutput.Data.Id, loggedOutput.Id);
            Assert.AreEqual(loginOutput.Data.Name, loggedOutput.Name);
            Assert.AreEqual(loginOutput.Data.Email, loggedOutput.Email);
            Assert.AreEqual(loginOutput.Data.Password, loggedOutput.Password);
            Assert.AreEqual(loginOutput.Data.Token, loggedOutput.Token);
        }

        //Test: If the logged service works perfectly with an token that does not exist
        [Test]
        public void LoggedError()
        {
            var reponseNotFound = accountController.Logged("token") as NotFoundObjectResult;
            Assert.IsNotNull(reponseNotFound);
            Assert.AreEqual(404, reponseNotFound.StatusCode);
        }

    }
}

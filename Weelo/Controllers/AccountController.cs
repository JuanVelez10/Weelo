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
    //In this class all the services associated with the account are consumed, for example the login
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private AccountLogic accountLogic;
        private ToolsConfig toolsConfig;

        public AccountController(IMapper mapper, IConfiguration iConfig)
        {
            this.mapper = mapper;
            accountLogic = new AccountLogic(mapper);
            config = iConfig;
            toolsConfig = new ToolsConfig();
        }

        // GET api/<AccountController>
        //Method to get all system accounts
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var accounts = new List<AccountEntity>();
            await Task.Run(() =>
            {
                accounts = accountLogic.GetAll();
            });

            if (accounts.Any()) return Ok(accounts);
            return NotFound();
        }

        // GET api/<AccountController>/f3a2ab2c-3b73-4fe9-a176-72d75973ea72
        //Method to get a specific account
        [HttpGet("{id}")]
        public IActionResult Get(Guid? Id)
        {
            var account = accountLogic.Get(Id);
            if (account != null) return Ok(account);
            return NotFound();
        }

        // GET api/<AccountController>
        //Method to get a specific logged account
        [HttpGet]
        [Route("Logged")]
        public IActionResult Logged()
        {
            var account = toolsConfig.GetToken(Request);
            if (account != null) account = accountLogic.Get(account.Id);
            string srtoken =string.Empty;
            toolsConfig.TryRetrieveToken(Request, out srtoken);
            account.Token = srtoken;
            if (account != null) return Ok(account);
            return NotFound();
        }

        // POST api/<AccountController>
        //In this method an account is validated for login and a jwt token is generated
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest  loginRequest)
        {
            var response = accountLogic.GetAccountLogin(mapper.Map<LoginEntity>(loginRequest));
            if(response != null)
            {
                response.Data.Token = toolsConfig.Generate(config, response.Data);
                return Ok(response);
            }

            return NotFound();
        }


    }
}

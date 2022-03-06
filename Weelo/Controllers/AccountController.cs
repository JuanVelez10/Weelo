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
    public class AccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private AccountLogic accountLogic;
        private ToolsConfig toolsConfig;

        //Controller
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
            return NotFound(accounts);
        }

        // GET api/<AccountController>/f3a2ab2c-3b73-4fe9-a176-72d75973ea72
        //Method to get a specific account
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public IActionResult Get(Guid? Id)
        {
            var account = accountLogic.Get(Id);
            if (account != null) return Ok(account);
            return NotFound(account);
        }

        // GET api/<AccountController>
        //Method to get a specific logged account
        [HttpGet]
        [Route("Logged")]
        [Authorize(Roles = "Admin,Client")]
        public IActionResult Logged(string token=null)
        {
            string srtoken = string.Empty;

            if (string.IsNullOrEmpty(token)) toolsConfig.TryRetrieveToken(Request, out srtoken);
            else srtoken = token;

            var account = toolsConfig.GetToken(srtoken);
            if (account != null && !string.IsNullOrEmpty(account.Email)) account = accountLogic.Get(account.Id);

            account.Token = srtoken;
            if (account != null && !string.IsNullOrEmpty(account.Email)) return Ok(account);
            return NotFound(null);
        }

        // POST api/<AccountController>
        //In this method an account is validated for login and a jwt token is generated
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest  loginRequest)
        {
            var response = accountLogic.GetAccountLogin(mapper.Map<LoginEntity>(loginRequest));
            if(response != null && response.Data != null)
            {
                response.Data.Token = toolsConfig.Generate(config, response.Data);
                return Ok(response);
            }

            return BadRequest(response);
        }


    }
}

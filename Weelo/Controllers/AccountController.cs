using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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
            this.accountLogic = new AccountLogic(mapper);
            this.config = iConfig;
            toolsConfig = new ToolsConfig();
        }

        // GET api/<AccountController>
        //Method to get all system accounts
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var accounts = accountLogic.GetAll();
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


        // POST api/<AccountController>
        //In this method an account is validated for login and a jwt token is generated
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest  loginRequest)
        {
            var account = accountLogic.GetAccountLogin(mapper.Map<LoginEntity>(loginRequest));
            if(account != null)
            {
                var loginResponse = mapper.Map<LoginResponse>(account);
                loginResponse.Token = toolsConfig.Generate(config,loginResponse);
                return Ok(loginResponse);
            }

            return NotFound();
        }

    }
}

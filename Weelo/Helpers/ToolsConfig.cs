using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WeeloAPI.References;
using WeeloCore.Entities;
using static WeeloCore.Helpers.EnumType;

namespace WeeloAPI.Helpers
{
    //Class for general api configuration methods
    public class ToolsConfig
    {
        //Method to generate token per account
        public string Generate(IConfiguration config,LoginResponse loginResponse)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt")["Key"]));
            var credencials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.UtcNow.AddMinutes(Convert.ToUInt32(config.GetSection("Jwt")["Time"]));

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, loginResponse.Id.ToString()),
                    new Claim(ClaimTypes.Name, loginResponse.Name),
                    new Claim(ClaimTypes.Email, loginResponse.Email),
                    new Claim(ClaimTypes.Role, loginResponse.RoleType.ToString())
            };

            var token = new JwtSecurityToken(
                config.GetSection("Jwt")["Issuer"],
                config.GetSection("Jwt")["Audience"],
                claims,
                expires: expires,
                signingCredentials: credencials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Method to get token by account
        public LoginResponse GetToken(HttpRequest request)
        {
            LoginResponse login = new LoginResponse();
            string srtoken;

            if (TryRetrieveToken(request, out srtoken))
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                if (handler.ReadToken(srtoken) is JwtSecurityToken token)
                {
                    login.Id = Guid.Parse(token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                    login.Name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                    login.Email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                    login.RoleType = (RoleType)Enum.Parse(typeof(RoleType), token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value);
                }
            }
            return login;
        }

        //Method to validate request authorization header
        private static bool TryRetrieveToken(HttpRequest request, out string token)
        {
            token = null;
            if (string.IsNullOrEmpty(request.Headers["Authorization"].ToString())) return false;
            var bearerToken = request.Headers["Authorization"].ToString();
            token = bearerToken.Replace("Bearer ","");
            return true;
        }


    }
}

using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeeloAPI.References;
using WeeloCore.Entities;
using WeeloCore.Helpers;
using static WeeloCore.Helpers.EnumType;

namespace WeeloAPI.Helpers
{
    //Class for general api configuration methods
    public class ToolsConfig
    {
        private Tools tools = new Tools();

        //Method to generate token per account
        public string Generate(IConfiguration config,AccountEntity account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt")["Key"]));
            var credencials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.UtcNow.AddMinutes(Convert.ToUInt32(config.GetSection("Jwt")["Time"]));

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.Name),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, account.RoleType.ToString())
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
        public AccountEntity GetToken(HttpRequest request)
        {
            AccountEntity account = new AccountEntity();
            string srtoken;

            if (TryRetrieveToken(request, out srtoken))
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                if (handler.ReadToken(srtoken) is JwtSecurityToken token)
                {
                    account.Id = Guid.Parse(token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                    account.Name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                    account.Email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                    account.RoleType = (RoleType)Enum.Parse(typeof(RoleType), token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value);
                }
            }
            return account;
        }

        //Method to validate request authorization header
        public  bool TryRetrieveToken(HttpRequest request, out string token)
        {
            token = null;
            if (string.IsNullOrEmpty(request.Headers["Authorization"].ToString())) return false;
            var bearerToken = request.Headers["Authorization"].ToString();
            token = bearerToken.Replace("Bearer ","");
            return true;
        }

        //Method to Validate Request image
        public BaseResponse<bool> ValidateRequest(HttpRequest request)
        {
            if (request.Form == null) return MessageResponse(4, MessageType.Error, "Data");
            if (!request.Form.Keys.Any()) return MessageResponse(4, MessageType.Error, "Id");
            if (!request.Form.Where(x => x.Key == "id").Any()) return MessageResponse(4, MessageType.Error, "Id");
            if (!request.Form.Files.Any()) return MessageResponse(4, MessageType.Error, "Image");
            if (!request.Form.Files.Where(x => x.Name == "image" && x.Length > 0).Any()) return MessageResponse(4, MessageType.Error, "Image");

            return new BaseResponse<bool>();

        }

        //Method to Validate property string
        public BaseResponse<bool> ValidateGuid(string id)
        {
            if (string.IsNullOrEmpty(id)) return MessageResponse(4, MessageType.Error, "Id");
            Guid guidProperty;
            if (!Guid.TryParse(id, out guidProperty)) return MessageResponse(5, MessageType.Error, "Id");

            return new BaseResponse<bool>();
        }

        //Method to Validate image string
        public BaseResponse<bool> ValidateFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return MessageResponse(4, MessageType.Error, "File");

            return new BaseResponse<bool>();
        }

        //Method to return response message
        public BaseResponse<bool> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            response.Code = code;
            response.Message = String.Format("{0} {1}", tools.GetMessage(code, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

        //Method to save an image in firebase storage
        public async Task<string> UpLoadImage(Stream stream, string fileName, IConfiguration config)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(config.GetSection("Storage")["ApiKey"]));
            var a = await auth.SignInWithEmailAndPasswordAsync(config.GetSection("Storage")["AuthEmail"], config.GetSection("Storage")["AuthPassword"]);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                 config.GetSection("Storage")["Bucket"],
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("Property")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

    }
}

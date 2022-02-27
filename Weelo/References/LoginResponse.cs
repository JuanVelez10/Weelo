using System;
using static WeeloCore.Helpers.EnumType;

namespace WeeloAPI.References
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleType RoleType { get; set; }
        public string Token { get; set; }
    }
}

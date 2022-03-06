using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeeloCore.Helpers;
using static WeeloCore.Helpers.EnumType;

namespace WeeloAPI.References
{
    public class LoginRequest : IValidatableObject
    {
        private Tools tools = new Tools();

        public LoginRequest(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Email)) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Email) });
            if (string.IsNullOrEmpty(Password)) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Password) });
        }
    }
}

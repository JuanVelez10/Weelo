using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Entities
{
    public class AccountEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Birthday { get; set; }
        public AccountType AccountType { get; set; }
        public RoleType RoleType { get; set; }
        public string Token { get; set; }
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class Account
    {
        public Account()
        {
            Properties = new HashSet<Property>();
            PropertyTraceOwnerNewNavigations = new HashSet<PropertyTrace>();
            PropertyTraceOwnerOldNavigations = new HashSet<PropertyTrace>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Birthday { get; set; }
        public int AccountType { get; set; }
        public int RoleType { get; set; }
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<PropertyTrace> PropertyTraceOwnerNewNavigations { get; set; }
        public virtual ICollection<PropertyTrace> PropertyTraceOwnerOldNavigations { get; set; }
    }
}

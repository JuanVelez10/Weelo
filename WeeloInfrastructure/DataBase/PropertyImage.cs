using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class PropertyImage
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool? Enabled { get; set; }
        public Guid IdProperty { get; set; }

        public virtual Property IdPropertyNavigation { get; set; }
    }
}

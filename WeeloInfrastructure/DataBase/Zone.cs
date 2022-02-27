using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class Zone
    {
        public Zone()
        {
            Properties = new HashSet<Property>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid IdCity { get; set; }

        public virtual City IdCityNavigation { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}

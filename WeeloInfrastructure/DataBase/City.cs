using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class City
    {
        public City()
        {
            Zones = new HashSet<Zone>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviative { get; set; }
        public Guid IdState { get; set; }

        public virtual State IdStateNavigation { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }
    }
}

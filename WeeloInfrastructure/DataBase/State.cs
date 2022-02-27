using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviative { get; set; }
        public Guid IdCountry { get; set; }

        public virtual Country IdCountryNavigation { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Indicative { get; set; }
        public string Abbreviative { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}

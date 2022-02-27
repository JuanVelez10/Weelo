using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Entities
{
    public class PropertyEntity
    {
        public Guid Id { get; set; }
        public Guid IdZone { get; set; }
        public int CodeInternal { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public string Type { get; set; }
        public ConditionType ConditionType { get; set; }
        public string Condition { get; set; }
        public SecurityType SecurityType { get; set; }
        public string Security { get; set; }
        public AreaType AreaType { get; set; }
        public string Area { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public int Garages { get; set; }
        public int TotalSquareFeet { get; set; }
        public bool Furnished { get; set; }
        public bool? SwimmingPool { get; set; }
        public bool? Gym { get; set; }
        public bool? Oceanfront { get; set; }
        public bool Enabled { get; set; }
        public string ImageUrl { get; set; }

    }
}

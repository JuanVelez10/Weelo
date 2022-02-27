using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class Property
    {
        public Property()
        {
            PropertyImages = new HashSet<PropertyImage>();
            PropertyTraces = new HashSet<PropertyTrace>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CodeInternal { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public bool Enabled { get; set; }
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }
        public int PropertyType { get; set; }
        public bool Furnished { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public int TotalSquareFeet { get; set; }
        public int Garages { get; set; }
        public bool? SwimmingPool { get; set; }
        public bool? Gym { get; set; }
        public bool? Oceanfront { get; set; }
        public int ConditionType { get; set; }
        public int SecurityType { get; set; }
        public int AreaType { get; set; }
        public bool? Elevator { get; set; }
        public int Floor { get; set; }
        public int Levels { get; set; }
        public Guid IdZone { get; set; }
        public Guid IdOwner { get; set; }

        public virtual Account IdOwnerNavigation { get; set; }
        public virtual Zone IdZoneNavigation { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<PropertyTrace> PropertyTraces { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeeloCore.Helpers;
using static WeeloCore.Helpers.EnumType;

namespace WeeloAPI.References
{
    public class PropertyRequest : IValidatableObject
    {
        private Tools tools = new Tools();
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public bool Enabled { get; set; } = false;
        public PropertyType PropertyType { get; set; } = PropertyType.None;
        public ConditionType ConditionType { get; set; } = ConditionType.None;
        public SecurityType SecurityType { get; set; } = SecurityType.None;
        public AreaType AreaType { get; set; } = AreaType.None;
        public bool Furnished { get; set; } = false;
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public int TotalSquareFeet { get; set; }
        public int Garages { get; set; }
        public bool? SwimmingPool { get; set; } = false;
        public bool? Gym { get; set; } = false;
        public bool? Oceanfront { get; set; } = false;
        public bool? Elevator { get; set; } = false;
        public int Floor { get; set; }
        public int Levels { get; set; }
        public Guid? IdZone { get; set; }
        public Guid? IdOwner { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IdZone.HasValue) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(IdZone) });
            if (!IdOwner.HasValue) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(IdOwner) });

            if (string.IsNullOrEmpty(Name)) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Name) });
            if (string.IsNullOrEmpty(Address)) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Address) });
            if (Latitude < -90 || Latitude > 90) yield return new ValidationResult(tools.GetMessage(5, MessageType.Error), new[] { nameof(Name) });
            if (Longitude < -180 || Longitude > 180) yield return new ValidationResult(tools.GetMessage(5, MessageType.Error), new[] { nameof(Name) });
            if (Year <= 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Year) });
            if (Price <= 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Price) });

            if (!Enum.IsDefined(typeof(PropertyType), PropertyType)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(PropertyType) });
            if (!Enum.IsDefined(typeof(ConditionType), ConditionType)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(ConditionType) });
            if (!Enum.IsDefined(typeof(SecurityType), SecurityType)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(SecurityType) });
            if (!Enum.IsDefined(typeof(AreaType), AreaType)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(AreaType) });

            if (Rooms <= 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Rooms) });
            if (Bathrooms <= 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Bathrooms) });
            if (TotalSquareFeet <= 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(TotalSquareFeet) });
            if (Garages <= 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Garages) });
            if (Floor <= 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Floor) });
            if (Levels <= 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Levels) });

        }


    }
}

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

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Required]
        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Year { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public bool Enabled { get; set; } = false;

        [Required]
        public PropertyType PropertyType { get; set; } = PropertyType.None;

        [Required]
        public ConditionType ConditionType { get; set; } = ConditionType.None;

        [Required]
        public SecurityType SecurityType { get; set; } = SecurityType.None;

        [Required]
        public AreaType AreaType { get; set; } = AreaType.None;

        [Required]
        public bool Furnished { get; set; } = false;

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Rooms { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Bathrooms { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int TotalSquareFeet { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Garages { get; set; }

        [Required]
        public bool? SwimmingPool { get; set; } = false;

        [Required]
        public bool? Gym { get; set; } = false;

        [Required]
        public bool? Oceanfront { get; set; } = false;

        [Required]
        public bool? Elevator { get; set; } = false;

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Floor { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Levels { get; set; }

        [Required]
        public Guid? IdZone { get; set; }

        [Required]
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

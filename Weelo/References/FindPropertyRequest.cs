using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeeloCore.Helpers;
using static WeeloCore.Helpers.EnumType;

namespace WeeloAPI.References
{
    public class FindPropertyRequest : IValidatableObject
    {
        private Tools tools = new Tools();

        [Required]
        public Guid? IdCity { get; set; }

        public Guid? IdZone { get; set; }

        [Required]
        [RegularExpression("[0-9]*",ErrorMessage = "Only numeric value")]
        public int YearMin { get; set; }

        [Required]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int YearMax { get; set; } = DateTime.Now.Year;

        [Required]
        [DataType(DataType.Currency)]
        public decimal PriceMin { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PriceMax { get; set; }

        [Required]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int RoomsMin { get; set; }

        [Required]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int RoomsMax { get; set; }

        [Required]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Page { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; } = PropertyType.None;
        [Required]
        public ConditionType ConditionType { get; set; } = ConditionType.None;
        [Required]
        public SecurityType SecurityType { get; set; } = SecurityType.None;
        [Required]
        public AreaType AreaType { get; set; } = AreaType.None;
        [Required]
        public WithFurnished WithFurnished { get; set; } = WithFurnished.Both;
        [Required]
        public WithGarages WithGarages { get; set; } = WithGarages.Both;
        [Required]
        public WithSwimmingPool WithSwimmingPool { get; set; } = WithSwimmingPool.Both;
        [Required]
        public WithGym WithGym { get; set; } = WithGym.Both;
        [Required]
        public WithOceanfront WithOceanfront { get; set; } = WithOceanfront.Both;
        [Required]
        public WithImages WithImages { get; set; } = WithImages.Both;
        [Required]
        public OrderProperty OrderProperty { get; set; } = OrderProperty.None;
        [Required]
        public EnabledProperty EnabledProperty { get; set; } = EnabledProperty.Both;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IdCity.HasValue) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(IdCity) });
            if (PriceMin > PriceMax) yield return new ValidationResult(tools.GetMessage(2,MessageType.Error), new[] { nameof(PriceMin), nameof(PriceMax) });
            if (YearMin > YearMax) yield return new ValidationResult(tools.GetMessage(2, MessageType.Error), new[] { nameof(YearMin), nameof(YearMax) });
            if (RoomsMin > RoomsMax) yield return new ValidationResult(tools.GetMessage(2, MessageType.Error), new[] { nameof(RoomsMin), nameof(RoomsMax) });
            if (!Enum.IsDefined(typeof(PropertyType), PropertyType)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(PropertyType) });
            if (!Enum.IsDefined(typeof(ConditionType), ConditionType)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(ConditionType) });
            if (!Enum.IsDefined(typeof(SecurityType), SecurityType)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(SecurityType) });
            if (!Enum.IsDefined(typeof(AreaType), AreaType)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(AreaType) });
            if (!Enum.IsDefined(typeof(WithFurnished), WithFurnished)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(WithFurnished) });
            if (!Enum.IsDefined(typeof(WithGarages), WithGarages)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(WithGarages) });
            if (!Enum.IsDefined(typeof(WithSwimmingPool), WithSwimmingPool)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(WithSwimmingPool) });
            if (!Enum.IsDefined(typeof(WithGym), WithGym)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(WithGym) });
            if (!Enum.IsDefined(typeof(WithOceanfront), WithOceanfront)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(WithOceanfront) });
            if (!Enum.IsDefined(typeof(WithImages), WithImages)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(WithImages) });
            if (!Enum.IsDefined(typeof(OrderProperty), OrderProperty)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(OrderProperty) });
            if (!Enum.IsDefined(typeof(EnabledProperty), EnabledProperty)) yield return new ValidationResult(tools.GetMessage(3, MessageType.Error), new[] { nameof(EnabledProperty) });
        }

    }
}

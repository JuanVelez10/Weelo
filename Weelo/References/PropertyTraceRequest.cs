using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeeloCore.Helpers;
using static WeeloCore.Helpers.EnumType;

namespace WeeloAPI.References
{
    public class PropertyTraceRequest : IValidatableObject
    {
        private Tools tools = new Tools();

        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? DateSale { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Tax { get; set; }

        [Required]
        public Guid? OwnerNew { get; set; }
        [Required]
        public Guid? OwnerOld { get; set; }
        [Required]
        public Guid? IdProperty { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!OwnerNew.HasValue) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(OwnerNew) });
            if (!OwnerOld.HasValue) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(OwnerOld) });
            if (!IdProperty.HasValue) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(IdProperty) });
            if (!DateSale.HasValue) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(DateSale) });
            if (string.IsNullOrEmpty(Name)) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Name) });
            if (Value < 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Value) });
            if (Tax < 0) yield return new ValidationResult(tools.GetMessage(4, MessageType.Error), new[] { nameof(Tax) });
        }


    }
}

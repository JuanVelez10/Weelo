using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeeloAPI.References
{
    public class PropertyImageRequest : IValidatableObject
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool? Enabled { get; set; }
        public Guid? IdProperty { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }


    }
}

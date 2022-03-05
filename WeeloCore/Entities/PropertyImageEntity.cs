using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloCore.Entities
{
    public  class PropertyImageEntity
    {
        public PropertyImageEntity() { }

        public PropertyImageEntity(string Url, Guid? IdProperty)
        {
            this.Url = Url;
            this.IdProperty = IdProperty;
            this.Enabled = true;
        }

        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        [Required]
        public bool? Enabled { get; set; }
        public Guid? IdProperty { get; set; }

    }
}

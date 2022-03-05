using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloCore.Entities
{
    public class PropertyTraceEntity
    {

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
        public string NameOwnerNew { get; set; }

        [Required]
        public Guid? OwnerOld { get; set; }
        [Required]

        public string NameOwnerOld { get; set; }

        public Guid? IdProperty { get; set; }

        public DateTime Create { get; set; }



    }
}

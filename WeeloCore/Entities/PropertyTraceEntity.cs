using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloCore.Entities
{
    public class PropertyTraceEntity
    {
        public Guid Id { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public DateTime Create { get; set; }
        public Guid? OwnerNew { get; set; }
        public string NameOwnerNew { get; set; }
        public Guid? OwnerOld { get; set; }
        public string NameOwnerOld { get; set; }

    }
}

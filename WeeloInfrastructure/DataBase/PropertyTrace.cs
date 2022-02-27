using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class PropertyTrace
    {
        public Guid Id { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public DateTime Create { get; set; }
        public Guid OwnerNew { get; set; }
        public Guid OwnerOld { get; set; }
        public Guid IdProperty { get; set; }

        public virtual Property IdPropertyNavigation { get; set; }
        public virtual Account OwnerNewNavigation { get; set; }
        public virtual Account OwnerOldNavigation { get; set; }
    }
}

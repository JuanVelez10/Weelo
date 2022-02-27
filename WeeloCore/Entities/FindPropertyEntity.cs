using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Entities
{
    public class FindPropertyEntity
    {
        public Guid? IdCity { get; set; }
        public Guid? IdZone { get; set; }
        public int YearMin { get; set; }
        public int YearMax { get; set; } = DateTime.Now.Year;
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
        public int RoomsMin { get; set; }
        public int RoomsMax { get; set; }
        public PropertyType PropertyType { get; set; } = PropertyType.None;
        public ConditionType ConditionType { get; set; } = ConditionType.None;
        public SecurityType SecurityType { get; set; } = SecurityType.None;
        public AreaType AreaType { get; set; } = AreaType.None;
        public WithFurnished WithFurnished { get; set; } = WithFurnished.Both;
        public WithGarages WithGarages { get; set; } = WithGarages.Both;
        public WithSwimmingPool WithSwimmingPool { get; set; } = WithSwimmingPool.Both;
        public WithGym WithGym { get; set; } = WithGym.Both;
        public WithOceanfront WithOceanfront { get; set; } = WithOceanfront.Both;
        public OrderProperty OrderProperty { get; set; } = OrderProperty.None;
    }
}

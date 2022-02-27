using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloCore.Entities
{
    public class ZoneInfoEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? IdCity { get; set; }
        public string NameCity { get; set; }
        public string AbbreviativeCity { get; set; }
        public Guid? IdState { get; set; }
        public string NameState { get; set; }
        public string AbbreviativeState { get; set; }
        public Guid? IdCountry { get; set; }
        public string NameCountry { get; set; }
        public string AbbreviativeCountry { get; set; }
    }
}

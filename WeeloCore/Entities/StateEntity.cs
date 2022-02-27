using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloCore.Entities
{
    public class StateEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviative { get; set; }
        public Guid? IdCountry { get; set; }
        public CountryEntity Country { get; set; }
    }
}

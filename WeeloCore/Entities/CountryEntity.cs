using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloCore.Entities
{
    public class CountryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Indicative { get; set; }
        public string Abbreviative { get; set; }
    }
}

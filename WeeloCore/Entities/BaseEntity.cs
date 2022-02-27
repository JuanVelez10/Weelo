using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloCore.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Message = string.Empty;
            Code = 0;
        }

        public int Code { get; set; }
        public string Message { get; set; }
    }
}

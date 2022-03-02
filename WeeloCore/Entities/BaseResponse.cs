using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Entities
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Message = string.Empty;
            Code = 0;
            MessageType = MessageType.None;
        }

        public int Code { get; set; }
        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public T Data { get; set; }

    }
}

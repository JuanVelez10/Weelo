using System;
using System.Collections.Generic;

#nullable disable

namespace WeeloInfrastructure.DataBase
{
    public partial class Message
    {
        public int Code { get; set; }
        public int MessageType { get; set; }
        public string Message1 { get; set; }
    }
}

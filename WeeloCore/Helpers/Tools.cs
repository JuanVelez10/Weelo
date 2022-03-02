using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;
using WeeloInfrastructure.Repositories;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Helpers
{
    //Class for general methods used by the logic layer
    public class Tools
    {
        MessageRepository messageRepository;

        public Tools()
        {
            messageRepository = new MessageRepository();
        }

        //Method to get success or error messages from database
        public string GetMessage(int Code, MessageType messageType)
        {
            return messageRepository.GetAll().Where(x => x.Code == Code && x.MessageType == (int)messageType).Select(x => x.Message1).FirstOrDefault();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    //This class is a repository that connects us to the database
    public class MessageRepository : GenericRepository<Message>
    {
        //Delete menssage from database
        public override Message Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Get message from database
        public override Message Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Get all message from database
        public override List<Message> GetAll()
        {
            return weeloDBContext.Messages.ToList();
        }

        //Add message from database
        public override Message Insert(Message @object)
        {
            throw new NotImplementedException();
        }

        //Update message from database
        public override Message Update(Message @object)
        {
            throw new NotImplementedException();
        }

    }
}

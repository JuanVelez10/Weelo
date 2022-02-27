using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>
    {
        public override Message Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override Message Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override List<Message> GetAll()
        {
            return weeloDBContext.Messages.ToList();
        }

        public override Message Insert(Message @object)
        {
            throw new NotImplementedException();
        }

        public override Message Update(Message @object)
        {
            throw new NotImplementedException();
        }

    }
}

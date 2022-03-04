using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class StateRepository : GenericRepository<State>
    {
        public override State Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override State Get(Guid? id)
        {
            return weeloDBContext.States.Where(x => x.Id == id).FirstOrDefault();
        }

        public override List<State> GetAll()
        {
            return weeloDBContext.States.ToList();
        }

        public override State Insert(State @object)
        {
            throw new NotImplementedException();
        }

        public override State Update(State @object)
        {
            throw new NotImplementedException();
        }
    }
}

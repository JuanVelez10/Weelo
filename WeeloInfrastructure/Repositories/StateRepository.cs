using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    //This class is a repository that connects us to the database
    public class StateRepository : GenericRepository<State>
    {
        //Delete state from database
        public override State Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Get state from database
        public override State Get(Guid? id)
        {
            return weeloDBContext.States.Where(x => x.Id == id).FirstOrDefault();
        }

        //Get all state from database
        public override List<State> GetAll()
        {
            return weeloDBContext.States.ToList();
        }

        //Add state from database
        public override State Insert(State @object)
        {
            throw new NotImplementedException();
        }

        //Update state from database
        public override State Update(State @object)
        {
            throw new NotImplementedException();
        }
    }
}

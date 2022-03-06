using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    //This class is a repository that connects us to the database
    public class CityRepository : GenericRepository<City>
    {
        //Delete city from database
        public override City Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Get account from database
        public override City Get(Guid? id)
        {
            return weeloDBContext.Cities.Where(x => x.Id == id).FirstOrDefault();
        }

        //Get all accounts from database
        public override List<City> GetAll()
        {
            return weeloDBContext.Cities.ToList();
        }

        //Add account from database
        public override City Insert(City @object)
        {
            throw new NotImplementedException();
        }

        //Update account from database
        public override City Update(City @object)
        {
            throw new NotImplementedException();
        }
    }
}

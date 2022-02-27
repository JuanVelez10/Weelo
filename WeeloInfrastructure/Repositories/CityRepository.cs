using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>
    {
        public override City Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override City Get(Guid? id)
        {
            return weeloDBContext.Cities.Where(x => x.Id == id).FirstOrDefault();
        }

        public override List<City> GetAll()
        {
            throw new NotImplementedException();
        }

        public override City Insert(City @object)
        {
            throw new NotImplementedException();
        }

        public override City Update(City @object)
        {
            throw new NotImplementedException();
        }
    }
}

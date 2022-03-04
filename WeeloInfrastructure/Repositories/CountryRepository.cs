using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class CountryRepository : GenericRepository<Country>
    {
        public override Country Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override Country Get(Guid? id)
        {
            return weeloDBContext.Countries.Where(x => x.Id == id).FirstOrDefault();
        }

        public override List<Country> GetAll()
        {
            return weeloDBContext.Countries.ToList();
        }

        public override Country Insert(Country @object)
        {
            throw new NotImplementedException();
        }

        public override Country Update(Country @object)
        {
            throw new NotImplementedException();
        }
    }
}

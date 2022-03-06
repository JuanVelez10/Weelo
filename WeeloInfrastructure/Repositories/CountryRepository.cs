using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    //This class is a repository that connects us to the database
    public class CountryRepository : GenericRepository<Country>
    {
        //Delete country from database
        public override Country Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Get account from database
        public override Country Get(Guid? id)
        {
            return weeloDBContext.Countries.Where(x => x.Id == id).FirstOrDefault();
        }

        //Get all accounts from database
        public override List<Country> GetAll()
        {
            return weeloDBContext.Countries.ToList();
        }

        //Add account from database
        public override Country Insert(Country @object)
        {
            throw new NotImplementedException();
        }

        //Update account from database
        public override Country Update(Country @object)
        {
            throw new NotImplementedException();
        }
    }
}

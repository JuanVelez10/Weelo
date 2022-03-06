using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    //This class is a repository that connects us to the database
    public class ZoneRepository : GenericRepository<Zone>
    {
        //Delete zone from database
        public override Zone Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Get property from database
        public override Zone Get(Guid? id)
        {
            return weeloDBContext.Zones.Where(x => x.Id == id).FirstOrDefault();
        }

        //Get all zones from database
        public override List<Zone> GetAll()
        {
            return weeloDBContext.Zones.ToList();
        }

        //Add property from database
        public override Zone Insert(Zone @object)
        {
            throw new NotImplementedException();
        }

        //Update property from database
        public override Zone Update(Zone @object)
        {
            throw new NotImplementedException();
        }

        //Get all zones for cities from database
        public List<Zone> GetAllForCity(Guid? idCity)
        {
            return weeloDBContext.Zones.Where(x => x.IdCity == idCity).ToList();
        }

    }
}

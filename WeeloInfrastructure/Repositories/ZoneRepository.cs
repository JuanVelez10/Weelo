using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class ZoneRepository : GenericRepository<Zone>
    {
        public override Zone Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override Zone Get(Guid? id)
        {
            return weeloDBContext.Zones.Where(x => x.Id == id).FirstOrDefault();
        }

        public override List<Zone> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Zone Insert(Zone @object)
        {
            throw new NotImplementedException();
        }

        public override Zone Update(Zone @object)
        {
            throw new NotImplementedException();
        }

        public List<Zone> GetAllForCity(Guid? idCity)
        {
            return weeloDBContext.Zones.Where(x => x.IdCity == idCity).ToList();
        }

    }
}

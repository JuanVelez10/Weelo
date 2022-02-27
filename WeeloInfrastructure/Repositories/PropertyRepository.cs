using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class PropertyRepository : GenericRepository<Property>
    {
        public override Property Delete(Guid? id)
        {
            var property = Get(id);
            property = weeloDBContext.Properties.Remove(property).Entity;
            weeloDBContext.SaveChanges();
            return property;
        }

        public override Property Get(Guid? id)
        {
            return weeloDBContext.Properties.Where(x=> x.Id == id).FirstOrDefault();
        }

        public override List<Property> GetAll()
        {
            return weeloDBContext.Properties.ToList();
        }

        public override Property Insert(Property @object)
        {
            var property = weeloDBContext.Properties.Add(@object).Entity;
            weeloDBContext.SaveChanges();
            return property;
        }

        public override Property Update(Property @object)
        {
            var property = weeloDBContext.Properties.Update(@object).Entity;
            weeloDBContext.SaveChanges();
            return property;
        }

        public List<Property> GetAllForZones(List<Guid> isZones)
        {
            return weeloDBContext.Properties.Where(x => isZones.Contains(x.IdZone)).ToList();
        }

    }
}

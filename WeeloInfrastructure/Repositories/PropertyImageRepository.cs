using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class PropertyImageRepository : GenericRepository<PropertyImage>
    {
        public override PropertyImage Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override PropertyImage Get(Guid? id)
        {
            return weeloDBContext.PropertyImages.Where(x => x.Id == id).FirstOrDefault();
        }

        public override List<PropertyImage> GetAll()
        {
            throw new NotImplementedException();
        }

        public override PropertyImage Insert(PropertyImage @object)
        {
            var propertyImage = weeloDBContext.PropertyImages.Add(@object).Entity;
            weeloDBContext.SaveChanges();
            return propertyImage;
        }

        public override PropertyImage Update(PropertyImage @object)
        {
            throw new NotImplementedException();
        }

        public PropertyImage UpdateEnable(Guid? id, bool enable)
        {
            var propertyImage = Get(id);
            propertyImage.Enabled = enable;
            weeloDBContext.SaveChanges();
            return propertyImage;
        }

        public PropertyImage GetFirstForIdProperty(Guid? idProperty)
        {
            return weeloDBContext.PropertyImages.Where(x => x.IdProperty == idProperty && x.Enabled.Value).FirstOrDefault();
        }

        public List<PropertyImage> GetAllForIdProperty(Guid? idProperty)
        {
            return weeloDBContext.PropertyImages.Where(x => x.IdProperty == idProperty && x.Enabled.Value).ToList();
        }

    }
}

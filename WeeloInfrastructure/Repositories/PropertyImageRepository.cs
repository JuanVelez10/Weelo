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
            throw new NotImplementedException();
        }

        public override List<PropertyImage> GetAll()
        {
            throw new NotImplementedException();
        }

        public override PropertyImage Insert(PropertyImage @object)
        {
            throw new NotImplementedException();
        }

        public override PropertyImage Update(PropertyImage @object)
        {
            throw new NotImplementedException();
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

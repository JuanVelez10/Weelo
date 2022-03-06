using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    //This class is a repository that connects us to the database
    public class PropertyImageRepository : GenericRepository<PropertyImage>
    {
        //Delete image of property from database
        public override PropertyImage Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Get image of property from database
        public override PropertyImage Get(Guid? id)
        {
            return weeloDBContext.PropertyImages.Where(x => x.Id == id).FirstOrDefault();
        }

        //Get all images of property from database
        public override List<PropertyImage> GetAll()
        {
            throw new NotImplementedException();
        }

        //Add image of property from database
        public override PropertyImage Insert(PropertyImage @object)
        {
            var propertyImage = weeloDBContext.PropertyImages.Add(@object).Entity;
            weeloDBContext.SaveChanges();
            return propertyImage;
        }

        //Update image of property from database
        public override PropertyImage Update(PropertyImage @object)
        {
            throw new NotImplementedException();
        }

        //Update enable a image of property from database
        public PropertyImage UpdateEnable(Guid? id, bool enable)
        {
            var propertyImage = Get(id);
            propertyImage.Enabled = enable;
            weeloDBContext.SaveChanges();
            return propertyImage;
        }

        //Get image for property from database
        public PropertyImage GetFirstForIdProperty(Guid? idProperty)
        {
            return weeloDBContext.PropertyImages.Where(x => x.IdProperty == idProperty && x.Enabled.Value).FirstOrDefault();
        }

        //Get all images for property from database
        public List<PropertyImage> GetAllForIdProperty(Guid? idProperty)
        {
            return weeloDBContext.PropertyImages.Where(x => x.IdProperty == idProperty && x.Enabled.Value).ToList();
        }

    }
}

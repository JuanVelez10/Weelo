using System;
using System.Collections.Generic;
using System.Linq;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    //This class is a repository that connects us to the database
    public class PropertyRepository : GenericRepository<Property>
    {
        //Delete property from database
        public override Property Delete(Guid? id)
        {
            var property = new Property();
            using (var dbContextTransaction = weeloDBContext.Database.BeginTransaction())
            {

                try
                {
                    property = Get(id);

                    var images = weeloDBContext.PropertyImages.Where(x => x.IdProperty == property.Id).ToList();
                    images.ForEach(x=> weeloDBContext.PropertyImages.Remove(x));

                    var traces = weeloDBContext.PropertyTraces.Where(x => x.IdProperty == property.Id).ToList();
                    traces.ForEach(x => weeloDBContext.PropertyTraces.Remove(x));

                    property = weeloDBContext.Properties.Remove(property).Entity;

                    weeloDBContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    property = null;
                }
            }

            return property;
        }

        //Get property from database
        public override Property Get(Guid? id)
        {
            return weeloDBContext.Properties.Where(x=> x.Id == id).FirstOrDefault();
        }

        //Get all properties from database
        public override List<Property> GetAll()
        {
            return weeloDBContext.Properties.ToList();
        }

        //Add property from database
        public override Property Insert(Property @object)
        {
            var date = DateTime.Now;
            @object.Create = date;
            @object.Update = date;
            var property = weeloDBContext.Properties.Add(@object).Entity;
            weeloDBContext.SaveChanges();
            return property;
        }

        //Update property from database
        public override Property Update(Property @object)
        {
            var property = Get(@object.Id);
            property.Update = DateTime.Now;
            property.Address = @object.Address;
            property.AreaType = (int)@object.AreaType;
            property.Bathrooms = @object.Bathrooms;
            property.ConditionType = (int)@object.ConditionType;
            property.Description = @object.Description;
            property.Elevator = @object.Elevator;
            property.Enabled = @object.Enabled;
            property.Floor = @object.Floor;
            property.Furnished = @object.Furnished;
            property.Garages = @object.Garages;
            property.Gym = @object.Gym;
            property.IdOwner = @object.IdOwner;
            property.IdZone = @object.IdZone;
            property.Latitude = @object.Latitude;
            property.Longitude = @object.Longitude;
            property.Name = @object.Name;
            property.Oceanfront = @object.Oceanfront;
            property.Price = @object.Price;
            property.PropertyType = (int)@object.PropertyType;
            property.Rooms = @object.Rooms;
            property.SecurityType = (int)@object.SecurityType;
            property.SwimmingPool = @object.SwimmingPool;
            property.Year = @object.Year;
            weeloDBContext.SaveChanges();
            return property;
        }

        //Update price of property from database
        public Property UpdatePrice(Guid? id, decimal price)
        {
            var property = Get(id);
            property.Update = DateTime.Now;
            property.Price = price;
            weeloDBContext.SaveChanges();
            return property;
        }

        //Update property from database
        public Property UpdateEnable(Guid? id, bool enable)
        {
            var property = Get(id);
            property.Update = DateTime.Now;
            property.Enabled = enable;
            weeloDBContext.SaveChanges();
            return property;
        }

        //Get all properties for zones from database
        public List<Property> GetAllForZones(List<Guid> isZones)
        {
            return weeloDBContext.Properties.Where(x => isZones.Contains(x.IdZone)).ToList();
        }

    }
}

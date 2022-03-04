using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class PropertyTraceRepository : GenericRepository<PropertyTrace>
    {
        public override PropertyTrace Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override PropertyTrace Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        public override List<PropertyTrace> GetAll()
        {
            throw new NotImplementedException();
        }

        public override PropertyTrace Insert(PropertyTrace @object)
        {
            var propertyTrace = new PropertyTrace();
            using (var dbContextTransaction = weeloDBContext.Database.BeginTransaction())
            {

                try
                {
                    var date = DateTime.Now;
                    @object.Create = date;
                    propertyTrace = weeloDBContext.PropertyTraces.Add(@object).Entity;

                    var property = weeloDBContext.Properties.Where(x => x.Id == @object.IdProperty).FirstOrDefault();
                    property.IdOwner = @object.OwnerNew;
                    property.Price = @object.Value;
                    property.Update = date;

                    weeloDBContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    propertyTrace = null;
                }
            }

            return propertyTrace;

        }

        public override PropertyTrace Update(PropertyTrace @object)
        {
            throw new NotImplementedException();
        }

        public List<PropertyTrace> GetAllForIdProperty(Guid? idProperty)
        {
            return weeloDBContext.PropertyTraces.Where(x => x.IdProperty == idProperty).ToList();
        }

    }
}

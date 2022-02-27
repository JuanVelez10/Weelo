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
            throw new NotImplementedException();
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

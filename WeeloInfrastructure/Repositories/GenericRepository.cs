using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloInfrastructure.Repositories
{
    //This abstract base class for inheriting methods to repositories, and database connection.
    public abstract class GenericRepository<TDomainObject>
    {
        public DataBase.WeeloDBContext weeloDBContext = new DataBase.WeeloDBContext();
        public abstract TDomainObject Get(Guid? id);
        public abstract List<TDomainObject> GetAll();
        public abstract TDomainObject Insert(TDomainObject @object);
        public abstract TDomainObject Delete(Guid? id);
        public abstract TDomainObject Update(TDomainObject @object);

    }
}

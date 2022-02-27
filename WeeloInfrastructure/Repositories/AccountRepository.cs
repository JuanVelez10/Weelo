using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    public class AccountRepository : GenericRepository<Account>
    {
        public override Account Delete(Guid? id)
        {
            var account = Get(id);
            account = weeloDBContext.Accounts.Remove(account).Entity;
            weeloDBContext.SaveChanges();
            return account;
        }

        public override Account Get(Guid? id)
        {
            return weeloDBContext.Accounts.Where(x => x.Id == id).FirstOrDefault();
        }

        public override List<Account> GetAll()
        {
            return weeloDBContext.Accounts.ToList();
        }

        public override Account Insert(Account @object)
        {
            throw new NotImplementedException();
        }

        public override Account Update(Account @object)
        {
            throw new NotImplementedException();
        }

        public Account GetForEmailAndPassword(string email, string password)
        {
            return weeloDBContext.Accounts.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
    }
}

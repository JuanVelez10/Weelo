using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloInfrastructure.DataBase;

namespace WeeloInfrastructure.Repositories
{
    //This class is a repository that connects us to the database
    public class AccountRepository : GenericRepository<Account>
    {
        //Delete account from database
        public override Account Delete(Guid? id)
        {
            var account = Get(id);
            account = weeloDBContext.Accounts.Remove(account).Entity;
            weeloDBContext.SaveChanges();
            return account;
        }

        //Get account from database
        public override Account Get(Guid? id)
        {
            return weeloDBContext.Accounts.Where(x => x.Id == id).FirstOrDefault();
        }

        //Get all accounts from database
        public override List<Account> GetAll()
        {
            return weeloDBContext.Accounts.ToList();
        }

        //Add account from database
        public override Account Insert(Account @object)
        {
            throw new NotImplementedException();
        }

        //Update account from database
        public override Account Update(Account @object)
        {
            throw new NotImplementedException();
        }

        //Get account with email and password from database
        public Account GetForEmailAndPassword(string email, string password)
        {
            return weeloDBContext.Accounts.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
    }
}

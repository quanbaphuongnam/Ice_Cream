using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class QuanLyUserServicesImpl : QuanLyUserServices
    {

        public DatabaseContext db;
        public QuanLyUserServicesImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public Account Find(int id)
        {
            return db.Accounts.SingleOrDefault(b => b.AccId == id);
        }

        public List<Account> FindAllUser()
        {
            return db.Accounts.ToList();
        }

        public Account Update(Account account)
        {

            db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return account;
        }
    }
}

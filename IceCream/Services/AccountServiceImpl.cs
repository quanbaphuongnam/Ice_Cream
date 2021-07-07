using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class AccountServiceImpl : AccountService
    {
        public DatabaseContext db;
        public AccountServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public List<Account> FindAllAccount()
        {
            return db.Accounts.ToList();
        }
        public Account Create(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
            return account;
        }
        public Account Login(string username, string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.AccUsername == username);
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.AccPassword))
                {
                    return account;
                }
            }
            return null;
        }

        public Account Find(string user)
        {
            return db.Accounts.SingleOrDefault(b => b.AccUsername == user);
        }

        public Account EditAcccount(Account account)
        {
            db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return account;
        }

        public Account checkemail(string username, string email)
        {
            var account = db.Accounts.SingleOrDefault(a => a.AccUsername == username && a.AccEmail == email);
            if (account != null)
            {
                return account;
            }
            return null;
        }
    }
}

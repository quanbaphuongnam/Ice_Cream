using IceCream.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class ProfileServiceImpl : ProfileService
    {
        public DatabaseContext db;
        public ProfileServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public Account EditAcccount(Account account)
        {
            db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return account;
        }

        public Account FindByIdProfile(int id)
        {
             return db.Accounts.SingleOrDefault(p => p.AccId == id);
        }
    }
}

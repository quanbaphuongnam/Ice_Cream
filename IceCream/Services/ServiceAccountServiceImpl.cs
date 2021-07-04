using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class ServiceAccountServiceImpl : ServiceAccountService
    {
        public DatabaseContext db;
        public ServiceAccountServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public List<ServiceAccount> FindAll()
        {
            return db.ServiceAccounts.ToList();
        }

        public ServiceAccount Create(ServiceAccount serviceAccount)
        {
            db.ServiceAccounts.Add(serviceAccount);
            db.SaveChanges();
            return serviceAccount;
        }
    }
}

using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class ManageInvoiceServicesImpl : ManageInvoiceServices
    {
        public DatabaseContext db;
        public ManageInvoiceServicesImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public List<InvoiceAccount> FindAll()
        {
            return db.InvoiceAccounts.ToList();
        }
    }
}

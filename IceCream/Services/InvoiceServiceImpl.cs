using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class InvoiceServiceImpl : InvoiceService
    {
        public DatabaseContext db;
        public InvoiceServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public List<InvoiceAccount> FindAll()
        {
            return db.InvoiceAccounts.ToList();
        }

        public InvoiceAccount Create(InvoiceAccount invoiceAccount)
        {
            db.InvoiceAccounts.Add(invoiceAccount);
            db.SaveChanges();
            return invoiceAccount;
        }

        public InvoiceAccount Find(string id)
        {
            return db.InvoiceAccounts.SingleOrDefault(b => b.InvId == id);
        }

        public InvoiceAccount Edit(InvoiceAccount invoiceAccount)
        {
            db.Entry(invoiceAccount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return invoiceAccount;
        }
    }
}

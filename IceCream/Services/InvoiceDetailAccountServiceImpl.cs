using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class InvoiceDetailAccountServiceImpl : InvoiceDetailAccountService
    {
        public DatabaseContext db;
        public InvoiceDetailAccountServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        List<InvoiceDetailAccount> InvoiceDetailAccountService.FindAll()
        {
            return db.InvoiceDetailAccounts.ToList();
        }

        public InvoiceDetailAccount Create(InvoiceDetailAccount invoiceDetailAccount)
        {
            db.InvoiceDetailAccounts.Add(invoiceDetailAccount);
            db.SaveChanges();
            return invoiceDetailAccount;
        }

        InvoiceDetailAccount InvoiceDetailAccountService.Find(int id)
        {
            return db.InvoiceDetailAccounts.SingleOrDefault(b => b.Id == id);
        }

        public InvoiceDetailAccount Edit(InvoiceDetailAccount invoiceDetailAccount)
        {
            db.Entry(invoiceDetailAccount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return invoiceDetailAccount;
        }
    }
}

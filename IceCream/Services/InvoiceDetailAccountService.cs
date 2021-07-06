using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface InvoiceDetailAccountService
    {
      
        List<InvoiceDetailAccount> FindAll();
        public InvoiceDetailAccount Create(InvoiceDetailAccount invoiceDetailAccount);
        InvoiceDetailAccount Find(int id);
        InvoiceDetailAccount Edit(InvoiceDetailAccount invoiceDetailAccount);

    }
}

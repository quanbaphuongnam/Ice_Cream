using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface InvoiceService
    {
      
        List<InvoiceAccount> FindAll();
        public InvoiceAccount Create(InvoiceAccount invoiceAccount);
        InvoiceAccount Find(string id);
        InvoiceAccount Edit(InvoiceAccount invoiceAccount);

    }
}

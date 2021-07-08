using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class InvoiceAccount
    {
        public InvoiceAccount()
        {
            InvoiceDetailAccounts = new HashSet<InvoiceDetailAccount>();
        }

        public string InvId { get; set; }
        public int? AccId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Addr { get; set; }
        public string Phone { get; set; }
        public int? InvTotal { get; set; }
        public string InvPayment { get; set; }
        public string InvStatus { get; set; }
        public DateTime? InvCreated { get; set; }

        public virtual Account Acc { get; set; }
        public virtual ICollection<InvoiceDetailAccount> InvoiceDetailAccounts { get; set; }
    }
}

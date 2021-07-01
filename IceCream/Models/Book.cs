using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class Book
    {
        public Book()
        {
            InvoiceDetailAccounts = new HashSet<InvoiceDetailAccount>();
        }

        public int BookId { get; set; }
        public string BookPhoto { get; set; }
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public int? BookPrice { get; set; }
        public int? BookQuantity { get; set; }
        public int? BookYear { get; set; }
        public DateTime? BookCreated { get; set; }
        public DateTime? BookUpdate { get; set; }
        public byte? BookStatus { get; set; }

        public virtual ICollection<InvoiceDetailAccount> InvoiceDetailAccounts { get; set; }

    }
}

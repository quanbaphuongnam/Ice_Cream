using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class InvoiceDetailAccount
    {
        public int Id { get; set; }
        public int? InvId { get; set; }
        public int? BookId { get; set; }
        public string ScName { get; set; }
        public int ScQuantity { get; set; }
        public string ScPhoto { get; set; }
        public int? ScPrice { get; set; }
        public int? Total { get; set; }

        public virtual Book Book { get; set; }
        public virtual InvoiceAccount Inv { get; set; }
    }
}

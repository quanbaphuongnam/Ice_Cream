using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class ServiceAccount
    {
        public int SerAccId { get; set; }
        public int? SerId { get; set; }
        public int? AccId { get; set; }
        public DateTime? SerAccCreated { get; set; }
        public DateTime? SerAccEnd { get; set; }
        public int? SerAccPrice { get; set; }

        public virtual Account Acc { get; set; }
        public virtual Service Ser { get; set; }
    }
}

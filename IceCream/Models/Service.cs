using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class Service
    {
        public Service()
        {
            ServiceAccounts = new HashSet<ServiceAccount>();
        }

        public int SerId { get; set; }
        public string SerName { get; set; }
        public string SerDescription { get; set; }
        public int? SerPrice { get; set; }

        public virtual ICollection<ServiceAccount> ServiceAccounts { get; set; }
    }
}

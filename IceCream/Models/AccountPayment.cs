using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class AccountPayment
    {
        public int AccPayId { get; set; }
        public int? AccId { get; set; }
        public string AccPayName { get; set; }
        public string AccPayAddr { get; set; }

        public virtual Account Acc { get; set; }
    }
}

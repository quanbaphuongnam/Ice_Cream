using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class SavourFormula
    {
        public int IdSavourFormula { get; set; }
        public string HashtagId { get; set; }
        public int? ForId { get; set; }

        public virtual Formula For { get; set; }
        public virtual Savour Hashtag { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class SavourBook
    {
        public int IdSavourBook { get; set; }
        public string HashtagId { get; set; }
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Savour Hashtag { get; set; }
    }
}

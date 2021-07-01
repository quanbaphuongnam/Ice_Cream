using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class FeedbackBook
    {
        public int FeedbackId { get; set; }
        public int? BookId { get; set; }
        public int? AccId { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public byte? FeedbackStatus { get; set; }

        public virtual Account Acc { get; set; }
        public virtual Book Book { get; set; }
    }
}

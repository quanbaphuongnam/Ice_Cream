using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class FeedbackFormula
    {
        public int FeedbackId { get; set; }
        public int? FormulaId { get; set; }
        public int? AccId { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public byte? FeedbackStatus { get; set; }

        public virtual Account Acc { get; set; }
        public virtual Formula Formula { get; set; }
    }
}

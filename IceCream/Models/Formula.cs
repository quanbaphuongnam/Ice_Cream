using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class Formula
    {
        public Formula()
        {
            FeedbackFormulas = new HashSet<FeedbackFormula>();
            PhotoFormulas = new HashSet<PhotoFormula>();
            SavourFormulas = new HashSet<SavourFormula>();
        }

        public int ForId { get; set; }
        public string ForCover { get; set; }
        public string ForName { get; set; }
        public string ForDescription { get; set; }
        public int? ForContributors { get; set; }
        public string ForCondition { get; set; }
        public byte? ForStatus { get; set; }
        public DateTime? ForCreated { get; set; }
        public DateTime? ForUpdate { get; set; }

        public virtual Account ForContributorsNavigation { get; set; }
        public virtual ICollection<FeedbackFormula> FeedbackFormulas { get; set; }
        public virtual ICollection<PhotoFormula> PhotoFormulas { get; set; }
        public virtual ICollection<SavourFormula> SavourFormulas { get; set; }
    }
}

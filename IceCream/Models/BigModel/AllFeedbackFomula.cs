using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Models.BigModel
{
    public class AllFeedbackFomula
    {
  
        public string AccUsername { get; set; }
        public string AccFullname { get; set; }
        public int FeedbackId { get; set; }
        public int? FormulaId { get; set; }
        public int? AccId { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public byte? FeedbackStatus { get; set; }
    }
}

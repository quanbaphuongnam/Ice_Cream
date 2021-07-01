using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Models.BigModel
{
    public class RecipeDetail
    {
        public int AccId { get; set; }
        public string AccUsername { get; set; }
        public string AccAvatar { get; set; }
        public int ForId { get; set; }
        public string ForCover { get; set; }
        public string ForName { get; set; }
        public string ForDescription { get; set; }
        public int? ForContributors { get; set; }
        public string ForCondition { get; set; }
        public byte? ForStatus { get; set; }
        public DateTime? ForCreated { get; set; }
        public DateTime? ForUpdate { get; set; }
        public int FeedbackId { get; set; }
        public int? FormulaId { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public byte? FeedbackStatus { get; set; }
        public FeedbackFormula FeedbackFormula { get; set; }
    }
}

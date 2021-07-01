using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Models.BigModel
{
    public class RecipeDetailModels
    {
        public Formula Formula { get; set; }
        public Account Account { get; set; }
        public Savour Savour { get; set; }
        public FeedbackFormula FeedbackFormula { get; set; }
        public SavourFormula SavourFormula { get; set; }
        public PhotoFormula PhotoFormula { get; set; }
    }
}

using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface FeedbackFomulaServices
    {
       FeedbackFormula FindFBFormula(int id);

        FeedbackFormula EditFormula(FeedbackFormula  feedbackFormula);
    }
}

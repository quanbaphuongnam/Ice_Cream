using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface RecipeService
    {
    
        Formula CreateFormula(Formula formula);
        PhotoFormula CreateFormulaListPhoTo(PhotoFormula photoFormula);
        FeedbackFormula CreateFeedbackFormula(FeedbackFormula fbFormula);
    }
}

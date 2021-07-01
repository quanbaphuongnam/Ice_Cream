using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class RecipeServiceImpl : RecipeService
    {
        public DatabaseContext db;
        public RecipeServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public FeedbackFormula CreateFeedbackFormula(FeedbackFormula fbFormula)
        {
            db.FeedbackFormulas.Add(fbFormula);
            db.SaveChanges();
            return fbFormula;
        }

        public Formula CreateFormula(Formula formula)
        {
            db.Formulas.Add(formula);
            db.SaveChanges();
            return formula;
        }

        public PhotoFormula CreateFormulaListPhoTo(PhotoFormula photoFormula)
        {
            db.PhotoFormulas.Add(photoFormula);
            db.SaveChanges();
            return photoFormula;
        }

     
    }
}

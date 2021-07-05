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
            fbFormula.Created = DateTime.Now;
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

      

     
    }
}

using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class FeedbackFomulaServicesImpl : FeedbackFomulaServices
    {
        public DatabaseContext db;
        public FeedbackFomulaServicesImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public FeedbackFormula EditFormula(FeedbackFormula feedbackFormula)
        {
            db.Entry(feedbackFormula).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
            return feedbackFormula;
        }

        public FeedbackFormula FindFBFormula(int id)
        {
            return db.FeedbackFormulas.SingleOrDefault(p => p.FeedbackId == id);
        }
    }

      
}

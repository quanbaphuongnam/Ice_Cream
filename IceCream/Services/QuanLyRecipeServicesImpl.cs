using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class QuanLyRecipeServicesImpl : QuanLyRecipeServices
    {
        public DatabaseContext db;
        public QuanLyRecipeServicesImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public Formula CreateFormula(Formula formula)
        {
            db.Formulas.Add(formula);
            db.SaveChanges();
            return formula;
        }

        public void Delete(int id)
        {
            db.Formulas.Remove(db.Formulas.Find(id));
            db.SaveChanges();
        }

        public Formula EditFormula(Formula formula)
        {
            db.Entry(formula).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
            return formula;
        }

        public Formula Find(int id)
        {
            return db.Formulas.Find(id);
        }

        public List<Formula> FindAllFormula()
        {
            return db.Formulas.ToList();
        }

        public Formula FindByIdFormula(int id)
        {
            return db.Formulas.SingleOrDefault(p => p.ForId == id);
        }
   
    }
}

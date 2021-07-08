using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface QuanLyRecipeServices
    {
        List<Formula> FindAllFormula();
        Formula CreateFormula(Formula formula);
        Formula Find(int id);
        void Delete(int id);
        Formula FindByIdFormula(int id);
        Formula EditFormula(Formula formula);
    }
}

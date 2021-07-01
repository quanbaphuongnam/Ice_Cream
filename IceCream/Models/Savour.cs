using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class Savour
    {
        public Savour()
        {
            SavourBooks = new HashSet<SavourBook>();
            SavourFormulas = new HashSet<SavourFormula>();
        }

        public string HashtagId { get; set; }
        public string SavName { get; set; }
        public string SavPhoto { get; set; }
        public string SavIngredients { get; set; }
        public string SavProcedure { get; set; }

        public virtual ICollection<SavourBook> SavourBooks { get; set; }
        public virtual ICollection<SavourFormula> SavourFormulas { get; set; }
    }
}

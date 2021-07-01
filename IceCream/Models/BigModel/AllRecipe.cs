using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Models.BigModel
{
    public class AllRecipe
    {
        public int AccId { get; set; }
        public string AccUsername { get; set; }
        public int ForId { get; set; }
        public string ForCover { get; set; }
        public string ForName { get; set; }
        public string ForDescription { get; set; }
        public int? ForContributors { get; set; }
        public string ForCondition { get; set; }
        public byte? ForStatus { get; set; }
        public DateTime? ForCreated { get; set; }
        public DateTime? ForUpdate { get; set; }
      
    }
}

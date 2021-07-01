using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class PhotoFormula
    {
        public int IdPhoto { get; set; }
        public int? ForId { get; set; }
        public string Img { get; set; }

        public virtual Formula For { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Models
{
    public class AccPaySignup
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Package { get; set; }
        public int Total { get; set; }
    }
}

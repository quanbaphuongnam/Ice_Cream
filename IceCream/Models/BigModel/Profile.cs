using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Models.BigModel
{
    public class Profile
    {
        public int AccId { get; set; }
        public string AccUsername { get; set; }
        public string AccPassword { get; set; }
        public string AccAvatar { get; set; }
        public string AccEmail { get; set; }
        public string AccPhone { get; set; }
        public string AccFullname { get; set; }
        public string AccGender { get; set; }
        public DateTime? AccDob { get; set; }
        public string AccAddr { get; set; }
    }
}

using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface ProfileService
    {
        //public Account FindProfile(int id);
        Account FindByIdProfile(int id);
        Account EditAcccount(Account account);
    }
}

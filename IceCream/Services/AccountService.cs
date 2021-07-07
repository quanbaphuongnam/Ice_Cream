using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface AccountService
    {
      
        List<Account> FindAllAccount();
        public Account Create(Account account);
        Account Find(string user);
        Account Login(string username, string password);
        Account checkemail(string username, string email);
        Account EditAcccount(Account account);

    }
}

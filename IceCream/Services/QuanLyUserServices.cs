using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface QuanLyUserServices
    {
        List<Account> FindAllUser();

        public Account Update(Account account);

        public Account Find(int id);
    }
}

using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface QuanLySavourServices
    {
        List<Savour> FindAllSavour();
        public Savour Update(Savour savour);

        public Savour Find(string HagId);

        void Delete(string id);
        Savour AddSavour(Savour savour);
    }
}

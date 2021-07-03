using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class QuanLySavourServicesImpl : QuanLySavourServices
    {
        public DatabaseContext db;
        public QuanLySavourServicesImpl(DatabaseContext _db)
        {
            db = _db;
        }
        public List<Savour> FindAllSavour()
        {
            return db.Savours.ToList();
        }
    }
}

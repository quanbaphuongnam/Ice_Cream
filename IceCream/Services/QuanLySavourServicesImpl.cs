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
        public void Delete(string id)
        {
            db.Savours.Remove(db.Savours.Find(id));
            db.SaveChanges();
        }


        public Savour Find(string HagId)
        {
            return db.Savours.SingleOrDefault(b => b.HashtagId == HagId);
        }



        public Savour Update(Savour savour)
        {
            db.Entry(savour).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return savour;
        }



        public Savour AddSavour(Savour savour)
        {
            db.Savours.Add(savour);
            db.SaveChanges();
            return savour;
        }
    }
}

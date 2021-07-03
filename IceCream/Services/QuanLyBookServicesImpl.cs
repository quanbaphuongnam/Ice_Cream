using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class QuanLyBookSercicesImpl : QuanLyBookServices
    {
        public DatabaseContext db;
        public QuanLyBookSercicesImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public Book Find(int id)
        {
            return db.Books.SingleOrDefault(b => b.BookId == id);
        }

      

        public List<Book> FindAllBook()
        {
            return db.Books.ToList();
        }

        //public bool Update(Book book)
        //{
        //  var user =   db.Books.Find(book.BookId);
        //    
        //    user.BookName = book.BookName;
        //    user.BookPrice = book.BookPrice;
        //    user.BookQuantity = book.BookQuantity;
        //    user.BookYear = book.BookYear;
        //    user.BookCreated = book.BookCreated;
        //    user.BookUpdate = DateTime.Now;
        //    user.BookStatus = book.BookStatus;
        //    db.SaveChanges();
        //    return true;
            
        //}
        public bool Update(Book book)
        {
            db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;

        }


    }
}

using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;

namespace IceCream.Services
{
    public class QuanLyBookSercicesImpl : QuanLyBookServices
    {
        public DatabaseContext db;
        public QuanLyBookSercicesImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public Book AddBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return book;
        }

        public bool Delete(int id)
        {
            db.Books.Remove(db.Books.Find(id));
            db.SaveChanges();
            return true;
        }

        public Book Find(int id)
        {
            return db.Books.SingleOrDefault(b => b.BookId == id);
        }



        public List<Book> FindAllBook()
        {
            return db.Books.ToList();
        }



        Book QuanLyBookServices.Update(Book book)
        {

            db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return book;
        }
    }
}

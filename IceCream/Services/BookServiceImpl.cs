using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class BookServiceImpl : BookService
    {
        public DatabaseContext db;
        public BookServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public Book Edit(Book book)
        {
            db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return book;
        }

        public Book Find(int id)
        {
            return db.Books.SingleOrDefault(b => b.BookId == id);
            
        }

        public List<Book> FindAllBook()
        {
            return db.Books.ToList();
        }
    }
}

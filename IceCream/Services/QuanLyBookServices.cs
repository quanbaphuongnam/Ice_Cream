using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface QuanLyBookServices
    {
        List<Book> FindAllBook();


        public Book Update(Book book);

        public Book Find(int id);

        public bool Delete(int id);

        Book AddBook(Book book);
    }
}

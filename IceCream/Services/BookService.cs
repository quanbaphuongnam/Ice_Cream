using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface BookService
    {
        List<Book> FindAllBook();

        Book Find(int id);
    }
}

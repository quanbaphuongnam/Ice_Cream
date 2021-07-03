using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("book")]
    public class BookController : Controller
    {
        private BookService bookService;
        private IWebHostEnvironment webHostEnvironment;


        public BookController(BookService _bookService, IWebHostEnvironment _webHostEnvironment)
        {
            bookService = _bookService;

        }


        [Route("allbook")]
        [Route("")]
        public IActionResult AllBook()
        {
            ViewBag.books = bookService.FindAllBook();
            return View("allbook");
        }
        [Route("bookdetails")]
        public IActionResult BookDetails(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = bookService.Find(id);
            if (book == null)
            {
                return View("~/Views/Home/Page404.cshtml");
            }
            ViewBag.books = bookService.FindAllBook();
            ViewBag.book = book;
            return View("bookdetails");
        }
    }
}

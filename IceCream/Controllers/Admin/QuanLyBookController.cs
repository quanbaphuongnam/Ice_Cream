using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("quanlybook")]
    public class QuanLyBookController : Controller
    {
        public DatabaseContext db;
        private QuanLyBookServices quanlybookServices;
        private IWebHostEnvironment webHostEnvironment;


        public QuanLyBookController(DatabaseContext _db, QuanLyBookServices _quanlybookServices, IWebHostEnvironment _webHostEnvironment)
        {
            db = _db;
            quanlybookServices = _quanlybookServices;
            webHostEnvironment = _webHostEnvironment;
        }
        [Route("quanlybook")]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.quanlybooks = quanlybookServices.FindAllBook();
            return View("quanlybook");
        }
        [HttpGet]
        [Route("edit/id")]
        public IActionResult Edit(int id)
        {
            ViewBag.quanLybook = from a in db.Books
                                 where a.BookId == id
                                 select a;

            return View("edit", (id));
        }
        [Route("edit")]
        [HttpPost]
        public IActionResult Edit(Book book, IFormFile file)
        {

            var currentBook = quanlybookServices.Find(book.BookId);

            if (file != null)
            {
                var fileName = System.Guid.NewGuid().ToString().Replace("-", "");
                var ext = file.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/books", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                currentBook.BookPhoto = fileName + "." + ext;

            }
            currentBook.BookName = book.BookName;
            currentBook.BookPrice = book.BookPrice;
            currentBook.BookQuantity = book.BookQuantity;
            currentBook.BookYear = book.BookYear;
            currentBook.BookCreated = book.BookCreated;
            currentBook.BookUpdate = DateTime.Now;
            currentBook.BookStatus = book.BookStatus;

            quanlybookServices.Update(currentBook);
            return RedirectToAction("quanlybook");
        }
    }
}

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
using PagedList;
using System.Diagnostics;

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

            if (HttpContext.Session.GetInt32("admin") != null)
            {
                ViewBag.quanlybooks = quanlybookServices.FindAllBook();
                return View("quanlybook");
            }
            return View("~/Views/Home/Page404.cshtml");
          
           
           
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int id)
        {

            ViewBag.book = quanlybookServices.Find(id);
            return View("edit");
        }
        [Route("edit")]
        [HttpPost]
        public IActionResult Edit(Book book, IFormFile filecover)
        {

            var currentBook = quanlybookServices.Find(book.BookId);
          

            if (filecover != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var ext = filecover.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/books", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    filecover.CopyTo(fileStream);
                }
                currentBook.BookPhoto = fileName + "." + ext;

            }
           

            if (book.BookName != null) { currentBook.BookName = book.BookName; }
            if (book.BookPrice != null) { currentBook.BookPrice = book.BookPrice; }
            if (book.BookQuantity != null) { currentBook.BookQuantity = book.BookQuantity; }
            if (book.BookYear != null) { currentBook.BookYear = book.BookYear; }
            if (book.BookCreated != null) { currentBook.BookCreated = book.BookCreated; }
            if (book.BookStatus != null) { currentBook.BookStatus = book.BookStatus; }
            //currentBook.BookName = book.BookName;
            //currentBook.BookPrice = book.BookPrice;
            //currentBook.BookQuantity = book.BookQuantity;
            //currentBook.BookYear = book.BookYear;
            //currentBook.BookCreated = book.BookCreated;
            //currentBook.BookStatus = book.BookStatus;
            //currentBook.BookUpdate = DateTime.Now;

            quanlybookServices.Update(currentBook);
            return RedirectToAction("quanlybook");
        }
        
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {

            quanlybookServices.Delete(id);
            return RedirectToAction("quanlybook");
        }
        [Route("add")]
        public IActionResult Add()
        {

            return View("add");
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Book book, IFormFile filecover)
        {
            if (filecover != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var ext = filecover.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/books", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    filecover.CopyTo(fileStream);
                }
                book.BookPhoto = fileName + "." + ext;

            }
            else
            {
                book.BookPhoto = "";
            }

            book.BookCreated = DateTime.Now;
            book.BookUpdate = DateTime.Now;

            quanlybookServices.AddBook(book);

            return RedirectToAction("quanlybook");
        }
    }
}

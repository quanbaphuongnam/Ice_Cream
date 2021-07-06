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
            //quanlybookServices.ListAllPaging(page, paseSize);
            ViewBag.quanlybooks = quanlybookServices.FindAllBook();
            //ViewBag.quanlybooks = quanlybookServices.ListAllPaging(page, paseSize);
            return View("quanlybook");
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
        [HttpDelete]
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
        public IActionResult Add(Book book, IFormFile file)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var ext = file.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/recipe", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                book.BookPhoto = fileName + "." + ext;

            }
            else
            {
                book.BookPhoto = "noimage.png";
            }


            book.BookCreated = DateTime.Now;
            book.BookUpdate = DateTime.Now;

            quanlybookServices.AddBook(book);

            return RedirectToAction("quanlybook");
        }
    }
}

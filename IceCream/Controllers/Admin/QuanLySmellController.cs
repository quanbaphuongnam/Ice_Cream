using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("quanlysmell")]
    public class QuanLySmellController : Controller
    {
        private QuanLySavourServices quanLySavourServices;
        private IWebHostEnvironment webHostEnvironment;
        public QuanLySmellController(QuanLySavourServices _quanlysavourService, IWebHostEnvironment _webHostEnvironment)
        {
            quanLySavourServices = _quanlysavourService;
            webHostEnvironment = _webHostEnvironment;
        }
        [Route("quanlysmell")]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.savours = quanLySavourServices.FindAllSavour();
            return View("quanlysmell");
        }
        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(string id)
        {

            ViewBag.savour = quanLySavourServices.Find(id);
            return View("edit");
        }
        [Route("edit")]
        [HttpPost]
        public IActionResult Edit(Savour savour, IFormFile file)
        {

            var cureentsav = quanLySavourServices.Find(savour.HashtagId);

            if (file != null)
            {
                var fileName = System.Guid.NewGuid().ToString().Replace("-", "");
                var ext = file.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/books", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                cureentsav.SavPhoto = fileName + "." + ext;

            }


            cureentsav.SavName = savour.SavName;
            cureentsav.SavIngredients = savour.SavIngredients;
            cureentsav.SavProcedure = savour.SavProcedure;


            quanLySavourServices.Update(cureentsav);
            return RedirectToAction("quanlysmell");
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string id)
        {

            quanLySavourServices.Delete(id);
            return RedirectToAction("quanlysmell");
        }
        [Route("add")]
        public IActionResult Add()
        {

            return View("add");
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Savour savour, IFormFile file)
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
                savour.SavPhoto = fileName + "." + ext;

            }
            else
            {
                savour.SavPhoto = "noimage.png";
            }




            quanLySavourServices.AddSavour(savour);

            return RedirectToAction("quanlybook");
        }
    }
}

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
            if (HttpContext.Session.GetInt32("admin") != null)
            {
                ViewBag.savours = quanLySavourServices.FindAllSavour();
                return View("quanlysmell");
            }
            return View("~/Views/Home/Page404.cshtml");
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
        public IActionResult Edit(Savour savour, IFormFile filecover)
        {

            var cureentsav = quanLySavourServices.Find(savour.HashtagId);

            if (filecover != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var ext = filecover.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/savour", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    filecover.CopyTo(fileStream);
                }
                cureentsav.SavPhoto = fileName + "." + ext;

            }
            

            if (savour.HashtagId != null) { cureentsav.HashtagId = savour.HashtagId; }
            if (savour.SavName != null) { cureentsav.SavName = savour.SavName; }
            if (savour.SavIngredients != null) { cureentsav.SavIngredients = savour.SavIngredients; }
            if (savour.SavProcedure !=null) { cureentsav.SavProcedure = savour.SavProcedure; }
          

            //cureentsav.SavName = savour.SavName;
            //cureentsav.SavIngredients = savour.SavIngredients;
            //cureentsav.SavProcedure = savour.SavProcedure;


            quanLySavourServices.Update(cureentsav);
            return RedirectToAction("quanlysmell");
        }
      
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
        public IActionResult Add(Savour savour, IFormFile filecover)
        {
            if (filecover != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var ext = filecover.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/savour", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    filecover.CopyTo(fileStream);
                }
                savour.SavPhoto = fileName + "." + ext;

            }
            else
            {
                savour.SavPhoto = "";
            }




            quanLySavourServices.AddSavour(savour);

            return RedirectToAction("quanlysmell");
        }
    }
}

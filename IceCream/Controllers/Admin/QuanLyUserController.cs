using Castle.Core.Configuration;
using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("quanlyuser")]
    public class QuanLyUserController : Controller
    {
        private QuanLyUserServices quanLyUserServices;
        private ServiceAccountService serviceAccountService;
        private IWebHostEnvironment webHostEnvironment;
        public DatabaseContext db;


        public QuanLyUserController(QuanLyUserServices _quanLyUserServices, ServiceAccountService _serviceAccountService, IWebHostEnvironment _webHostEnvironment, DatabaseContext _db)
        {
            quanLyUserServices = _quanLyUserServices;
            webHostEnvironment = _webHostEnvironment;
            serviceAccountService = _serviceAccountService;
            db = _db;

        }
        [Route("")]
        [Route("quanlyuser")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("admin") != null)
            {
                ViewBag.User = quanLyUserServices.FindAllUser();
                ViewBag.SerAccount = serviceAccountService.FindAll();
                ViewBag.services = db.Services.ToList();
                return View("quanlyuser");
            }
            return View("~/Views/Home/Page404.cshtml");
          
        }

        [HttpDelete]
        [Route("delete/{idser}")]
        public IActionResult Delete(int idser)
        {

            serviceAccountService.Delete(idser);
            return RedirectToAction("quanlyuser");
        }
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {

            var currentUser = quanLyUserServices.Find(id);
            if(currentUser.AccStatus == 1)
            {
                currentUser.AccStatus = 0;
            }
            else
            {
                currentUser.AccStatus = 1;
            }
            Debug.WriteLine(currentUser.AccStatus);
            quanLyUserServices.Update(currentUser);
            return RedirectToAction("quanlyuser");
        }
    }
}

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

        private IWebHostEnvironment webHostEnvironment;


        public QuanLyUserController(QuanLyUserServices _quanLyUserServices, IWebHostEnvironment _webHostEnvironment)
        {
            quanLyUserServices = _quanLyUserServices;
            webHostEnvironment = _webHostEnvironment;

        }
        [Route("")]
        [Route("quanlyuser")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("admin") != null)
            {
                ViewBag.User = quanLyUserServices.FindAllUser();
                return View("quanlyuser");
            }
            return View("~/Views/Home/Page404.cshtml");
          
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

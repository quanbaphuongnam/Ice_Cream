using Castle.Core.Configuration;
using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [Route("quanlyuser")]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.User = quanLyUserServices.FindAllUser();
            return View("quanlyuser");
        }
        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int id)
        {


            return View("quanlyuser", quanLyUserServices.Find(id));
        }
        [Route("edit")]
        [HttpPost]
        public IActionResult Edit(Account account)
        {

            var currentUser = quanLyUserServices.Find(account.AccId);


            currentUser.AccStatus = account.AccStatus;

            quanLyUserServices.Update(currentUser);
            return RedirectToAction("quanlyuser");
        }
    }
}

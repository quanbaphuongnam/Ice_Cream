using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        }
        [Route("quanlysmell")]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.savours = quanLySavourServices.FindAllSavour();
            return View("quanlysmell");
        }
    }
}

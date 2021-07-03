using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("quanlyrecipe")]
    public class QuanLyRecipeController : Controller
    {

        private QuanLyRecipeServices quanlyrecipeServices;
        private IWebHostEnvironment webHostEnvironment;


        public QuanLyRecipeController(QuanLyRecipeServices _quanlyrecipeService, IWebHostEnvironment _webHostEnvironment)
        {

            quanlyrecipeServices = _quanlyrecipeService;
        }
        [Route("quanlyrecipe")]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.fomula = quanlyrecipeServices.FindAllFormula();
            return View("quanlyrecipe");
        }
    }
}

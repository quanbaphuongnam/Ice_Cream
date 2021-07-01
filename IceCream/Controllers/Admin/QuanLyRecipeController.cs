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
        [Route("quanlyrecipe")]
        [Route("")]
        public IActionResult Index()
        {
            return View("quanlyrecipe");
        }
    }
}

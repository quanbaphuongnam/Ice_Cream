using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("quanlybook")]
    public class QuanLyBookController : Controller
    {
        [Route("quanlybook")]
        [Route("")]
        public IActionResult Index()
        {
            return View("quanlybook");
        }
    }
}

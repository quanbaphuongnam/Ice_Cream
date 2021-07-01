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
        [Route("quanlyuser")]
        [Route("")]
        public IActionResult Index()
        {
            return View("quanlyuser");
        }
    }
}

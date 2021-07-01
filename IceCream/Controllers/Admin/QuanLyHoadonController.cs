using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("quanlyhoadon")]
    public class QuanLyHoadonController : Controller
    {
        [Route("quanlyhoadon")]
        [Route("")]
        public IActionResult Index()
        {
            return View("quanlyhoadon");
        }
    }
}

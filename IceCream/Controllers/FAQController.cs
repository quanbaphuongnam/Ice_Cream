using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("FAQ")]
    public class FAQController : Controller
    {
        [Route("FAQ")]
        [Route("")]
        public IActionResult Index()
        {
            return View("FAQ");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("aboutus")]
    public class AboutusController : Controller
    {
        [Route("aboutus")]
        [Route("cart")]
        public IActionResult Index()
        {
            return View("aboutus");
        }
    }
}

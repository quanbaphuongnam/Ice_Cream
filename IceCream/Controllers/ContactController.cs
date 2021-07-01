using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        [Route("contact")]
        [Route("")]
        public IActionResult Index()
        {
            return View("contact");
        }
    }
}

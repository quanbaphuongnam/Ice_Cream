using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("profile")]
    public class ProfileController : Controller
    {
        [Route("profile")]
        [Route("")]
        public IActionResult Index()
        {
            return View("profile");
        }
    }
}

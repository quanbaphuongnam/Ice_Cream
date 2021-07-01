using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Areas.Admin.Controllers
{
   
    [Route("homeadmin")]
    public class HomeAdminController : Controller
    {
        
        [Route("homeadmin")]
        [Route("")]
        public IActionResult Index()
        {
            return View("homeadmin");
        }
    }
}

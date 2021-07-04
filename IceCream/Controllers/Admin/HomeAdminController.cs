using Microsoft.AspNetCore.Http;
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
            if (HttpContext.Session.GetInt32("admin") != null)
            {
                return View("homeadmin");
            }
            return View("~/Views/Home/Page404.cshtml");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("approverecipe")]
    public class ApproveRecipeController : Controller
    {
        [Route("approverecipe")]
        [Route("")]
        public IActionResult Index()
        {
            return View("approverecipe");
        }
    }
}

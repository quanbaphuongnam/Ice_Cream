using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public DatabaseContext db;

        private ProfileService profileService;

        private IWebHostEnvironment webHostEnvironment;
        public ProfileController(DatabaseContext _db, ProfileService _profileService, IWebHostEnvironment _webHostEnvironment)
        {
           
            db = _db;
            profileService = _profileService;
            webHostEnvironment = _webHostEnvironment;
        }

        [Route("profile/{id}")]
        [Route("")]
        public IActionResult Index(int id)
        {

            if (HttpContext.Session.GetInt32("account") == id)
            {
                ViewBag.profile = from a in db.Accounts
                                  where a.AccId == id
                                  select a;
            }
            else
            {
                return View("~/Views/Home/Page404.cshtml");
            }
           
            return View("profile");
        }
    }
}

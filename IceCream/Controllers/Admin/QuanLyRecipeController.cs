using IceCream.Models;
using IceCream.Models.BigModel;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("quanlyrecipe")]
    public class QuanLyRecipeController : Controller
    {

        private QuanLyRecipeServices quanlyrecipeServices;
        private IWebHostEnvironment webHostEnvironment;
        public DatabaseContext db;

        public QuanLyRecipeController(DatabaseContext _db, QuanLyRecipeServices _quanlyrecipeService, IWebHostEnvironment _webHostEnvironment)
        {
            db = _db;
            quanlyrecipeServices = _quanlyrecipeService;
        }
        [Route("quanlyrecipe")]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.fomula = (from f in db.Formulas
                              join a in db.Accounts on f.ForContributors equals a.AccId
                              select new AllRecipe
                              {
                                  AccId = a.AccId,
                                  AccUsername = a.AccUsername,
                                  ForId = f.ForId,
                                  ForCover = f.ForCover,
                                  ForName = f.ForName,
                                  ForDescription = f.ForDescription,
                                  ForCondition = f.ForCondition,
                                  ForStatus = f.ForStatus,
                                  ForCreated = f.ForCreated
                              }).OrderByDescending(f => f.ForId);
            return View("quanlyrecipe");
        }
    }
}

using IceCream.Models;
using IceCream.Models.BigModel;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
            webHostEnvironment = _webHostEnvironment;
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
        [Route("addrecipe")]
        public IActionResult Addrecipe()
        {
         
            return View("addrecipe");
        }
        [HttpPost]
        [Route("addrecipe")]
        public IActionResult AddRecipe(Formula formula, IFormFile filecover)
        {
            if (filecover != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var ext = filecover.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/recipe", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    filecover.CopyTo(fileStream);
                }
                formula.ForCover = fileName + "." + ext;

            }
            else
            {
                formula.ForCover = "";
            }
         

            formula.ForCreated = DateTime.Now;
            formula.ForUpdate = DateTime.Now;

            quanlyrecipeServices.CreateFormula(formula);
            
            return RedirectToAction("addrecipe");
        }
        [Route("updaterecipe")]
        public IActionResult Updaterecipe()
        {

            return View("updaterecipe");
        }
        [Route("delete/{id}")]

        public IActionResult Delete(int id)
        {
            quanlyrecipeServices.Delete(id);
            return RedirectToAction("quanlyrecipe");
        }
    }
}

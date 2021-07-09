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
            if (HttpContext.Session.GetInt32("admin") != null)
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
                                  }).OrderBy(f => f.ForStatus);
                return View("quanlyrecipe");
            }
            return View("~/Views/Home/Page404.cshtml");
           
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
        [HttpGet]
        [Route("updaterecipe")]
        public IActionResult Updaterecipe(int id)
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
                              }).Where(f => f.ForId == id);
            return View("updaterecipe", quanlyrecipeServices.FindByIdFormula(id));
        }
        [Route("updaterecipe")]
        public IActionResult Updaterecipe(Formula formula , IFormFile filecover)
        {
          

            var currentRecipe = quanlyrecipeServices.FindByIdFormula(formula.ForId);

           
            if (filecover != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var ext = filecover.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img/recipe", fileName + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    filecover.CopyTo(fileStream);
                }
                currentRecipe.ForCover = fileName + "." + ext;

            }
            else if(formula.ForCover != null)
            {
                currentRecipe.ForCover = formula.ForCover;

            }
          

            if (formula.ForName != null) { currentRecipe.ForName = formula.ForName; }
            if (formula.ForDescription != null) { currentRecipe.ForDescription = formula.ForDescription; }
            if (formula.ForContributors != null) { currentRecipe.ForContributors = formula.ForContributors; }
            if (formula.ForCondition != null) { currentRecipe.ForCondition = formula.ForCondition; }
            if (formula.ForStatus != null) { currentRecipe.ForStatus = formula.ForStatus; }
            currentRecipe.ForUpdate = DateTime.Now; 
            quanlyrecipeServices.EditFormula(currentRecipe);
      
            return RedirectToAction("updaterecipe",new { id = formula.ForId });
        }
      
      
       
        
        [Route("activerecipe/{id}")]
        public IActionResult Activerecipe(Formula formula,int id)
        {


            var currentRecipe = quanlyrecipeServices.FindByIdFormula(id);


         
            currentRecipe.ForStatus = 1;
            currentRecipe.ForUpdate = DateTime.Now;
            quanlyrecipeServices.EditFormula(currentRecipe);

            return RedirectToAction("quanlyrecipe");
        }
        [Route("delete/{id}")]

        public IActionResult Delete(int id)
        {
            quanlyrecipeServices.Delete(id);
            return RedirectToAction("quanlyrecipe");
        }
    }
}

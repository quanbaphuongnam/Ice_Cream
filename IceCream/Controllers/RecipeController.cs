using IceCream.Models;
using IceCream.Models.BigModel;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Account;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("recipe")]
    public class RecipeController : Controller
    {
        public DatabaseContext db;
        

        private RecipeService recipeService;
        private SavourService savourService;
        private IWebHostEnvironment webHostEnvironment;


        public RecipeController(DatabaseContext _db,SavourService _savourService, RecipeService _recipeService,IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
            savourService = _savourService;
            recipeService = _recipeService;
            db = _db;
        }
        [Route("recipe")]
        [Route("")]
   
        public IActionResult Index( string searchString)
        {
            ViewBag.searchString =  searchString;
            //--------------------
            ViewBag.savours = savourService.FindAllSavour();

            ViewBag.newrecipes = (from f in db.Formulas
                                  join a in db.Accounts on f.ForContributors equals a.AccId
                                  where (f.ForStatus == 1)
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

                                      //ConLai = (x.Soluong - y.Soluong)
                                  }).OrderByDescending(f => f.ForId).Take(3);

            if (!String.IsNullOrEmpty(searchString))
                {

                ViewBag.recipes = from f in db.Formulas
                                  join a in db.Accounts on f.ForContributors equals a.AccId
                                  where (f.ForStatus == 1 && f.ForName.Contains(searchString) || a.AccUsername.Contains(searchString))
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
                                      //ConLai = (x.Soluong - y.Soluong)
                                  };
            }
            else
            {
                
                ViewBag.recipes = from f in db.Formulas
                                  join a in db.Accounts on f.ForContributors equals a.AccId
                                  where (f.ForStatus == 1)
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
                                      //ConLai = (x.Soluong - y.Soluong)
                                  };

                
            }
               
           
            //-------------------
           
            return View("recipe");
        }
        [Route("recipedetail/{id}")]
        [HttpGet]
        public IActionResult RecipeDetail(int id)
        {
            ViewBag.savours = savourService.FindAllSavour();
            ViewBag.newrecipes = (from f in db.Formulas
                                  join a in db.Accounts on f.ForContributors equals a.AccId
                                  where (f.ForStatus == 1)
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

                                      //ConLai = (x.Soluong - y.Soluong)
                                  }).OrderByDescending(f => f.ForId).Take(3);
            ViewBag.recipedetailfeedback = (from f in db.Formulas
                                            join fb in db.FeedbackFormulas on f.ForId equals fb.FormulaId
                                            join a in db.Accounts on fb.AccId equals a.AccId
                                            where (f.ForId == id)
                               select new RecipeDetail
                               {
                                   AccId = a.AccId,
                                   AccUsername = a.AccUsername,
                                   AccAvatar = a.AccAvatar,
                                   ForId = f.ForId,
                                   ForCover = f.ForCover,
                                   ForName = f.ForName,
                                   ForDescription = f.ForDescription,
                                   ForCondition = f.ForCondition,
                                   ForStatus = f.ForStatus,
                                   ForCreated = f.ForCreated,
                                   FeedbackId = fb.FeedbackId,
                                   Content = fb.Content,
                                   Created = fb.Created,
                                   FeedbackStatus = fb.FeedbackStatus
                                   //ConLai = (x.Soluong - y.Soluong)

                               });
         
           
            ViewBag.recipedetails = (from f in db.Formulas
                                     join a in db.Accounts on f.ForContributors equals a.AccId

                                     where (f.ForId == id)
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
                                         ForCreated = f.ForCreated,
                                         //ConLai = (x.Soluong - y.Soluong)
                                     });
            ViewBag.SLfb = db.FeedbackFormulas.Where(fb => fb.FormulaId == id).Count();
            return View("recipedetail");
        }

        [HttpPost]
        [Route("postrecipe")]
        public IActionResult CreateRecipe(Formula formula,PhotoFormula photoFormula, IFormFile filecover, IFormFile[] filecovers)
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
            if (filecovers != null && filecovers.Length > 0)
            {
                foreach (var photo in filecovers)
                {
                    Debug.WriteLine($"File Name: {photo.FileName}");
                    Debug.WriteLine($"ContentType: {photo.ContentType}");
                    Debug.WriteLine($"Length: {photo.Length}");

                    var fileNames = System.Guid.NewGuid().ToString();
                    Debug.WriteLine($"File Name: {fileNames}");

                    var exts = photo.ContentType.Split(new char[] { '/' })[1];
                    Debug.WriteLine($"ext: {exts}");

                    var paths = Path.Combine(webHostEnvironment.WebRootPath, "img/recipes", fileNames + "." + exts);
                    using (var fileStream = new FileStream(paths, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    photoFormula.Img = fileNames + "." + exts;
                }
            }

            formula.ForCreated = DateTime.Now;
            formula.ForUpdate = DateTime.Now;
            
            recipeService.CreateFormula(formula);
            recipeService.CreateFormulaListPhoTo(photoFormula);
            return RedirectToAction("recipe");
        }

        [HttpPost]
        [Route("feedbackrecipe")]
        public IActionResult FeedbackRecipe(FeedbackFormula fbFormula)
        {

            recipeService.CreateFeedbackFormula(fbFormula);

            return RedirectToAction("recipe");
        }
    }
}

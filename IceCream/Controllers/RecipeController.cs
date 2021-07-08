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
using PagedList.Mvc;
using System.Threading.Tasks;
using PagedList;

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
   
        public IActionResult Index( string searchString, int page = 1)
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
                int size = 9;
                var recipess = from f in db.Formulas
                                  join a in db.Accounts on f.ForContributors equals a.AccId
                                  join sf in db.SavourFormulas on f.ForId equals sf.ForId
                                  join s in db.Savours on sf.HashtagId equals s.HashtagId
                                  where (f.ForStatus == 1 && f.ForName.Contains(searchString) || a.AccUsername.Contains(searchString) || s.SavName.Contains(searchString))
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
                                      HashtagId = s.HashtagId,
                                      SavName = s.SavName
                                      //ConLai = (x.Soluong - y.Soluong)
                                  };
                ViewBag.Pages = recipess.Count();
                ViewBag.Page = page;
                ViewBag.recipes = recipess.Skip((page - 1) * size).Take(size).ToList();
                ViewBag.recipesCount = recipess.Skip((page - 1) * size).Take(size).ToList().Count();
            }
            else
            {
                int size = 9;
                var recipess = from f in db.Formulas
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

                ViewBag.Pages = recipess.Count();
                ViewBag.Page = page;
                ViewBag.recipes = recipess.Skip((page - 1) * size).Take(size).ToList();
                ViewBag.recipesCount = recipess.Skip((page - 1) * size).Take(size).ToList().Count();
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
            ViewBag.SLfb = db.FeedbackFormulas.Where(fb => fb.FormulaId == id ).Count();
            ViewBag.recipedetailsavour = (from f in db.Formulas
                                          join sf in db.SavourFormulas on f.ForId equals sf.ForId
                                          join s in db.Savours on sf.HashtagId equals s.HashtagId
                                          where (f.ForId == id)
                                          select new Savour
                                          {

                                              HashtagId = s.HashtagId,
                                              SavName = s.SavName

                                              //ConLai = (x.Soluong - y.Soluong)

                                          }).ToList(); 
            return View("recipedetail");
        }

        [HttpPost]
        [Route("postrecipe")]
        public IActionResult CreateRecipe(Formula formula,PhotoFormula photoFormula, IFormFile filecover)
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
            
            recipeService.CreateFormula(formula);
            ViewBag.messenge = 1;
            return RedirectToAction("recipe");
        }

        [HttpPost]
        [Route("feedbackrecipe")]
        public IActionResult FeedbackRecipe(FeedbackFormula fbFormula)
        {
            Debug.WriteLine("fbFormula :"+ fbFormula.FormulaId);
            recipeService.CreateFeedbackFormula(fbFormula);

            return RedirectToAction("recipedetail", new { id = fbFormula.FormulaId });
        }
    }
}

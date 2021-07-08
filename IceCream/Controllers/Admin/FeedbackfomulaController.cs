using IceCream.Models;
using IceCream.Models.BigModel;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("feedbackfomula")]
    public class FeedbackfomulaController : Controller
    {
        public DatabaseContext db;
        public FeedbackFomulaServices feedbackFomulaServices;
        private IWebHostEnvironment webHostEnvironment;
        public FeedbackfomulaController(DatabaseContext _db, IWebHostEnvironment _webHostEnvironment, FeedbackFomulaServices _feedbackFomulaServices)
        {
            webHostEnvironment = _webHostEnvironment;
            feedbackFomulaServices = _feedbackFomulaServices;
            db = _db;
        }
        [Route("feedbackfomula")]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("admin") != null)
            {
                ViewBag.fbfomula = from fb in db.FeedbackFormulas
                                   join a in db.Accounts on fb.AccId equals a.AccId
                                   where (a.AccStatus == 1)
                                   select new AllFeedbackFomula
                                   {
                                       AccId = a.AccId,
                                       AccUsername = a.AccUsername,
                                       AccFullname = a.AccFullname,
                                       FeedbackId = fb.FeedbackId,
                                       FormulaId = fb.FormulaId,
                                       Content = fb.Content,
                                       Created = fb.Created,
                                       FeedbackStatus = fb.FeedbackStatus
                                       //ConLai = (x.Soluong - y.Soluong)
                                   };
                return View("feedbackfomula");
            }
            return View("~/Views/Home/Page404.cshtml");
     
        }
        [Route("activefb/{id}")]
        public IActionResult Activerecipe(FeedbackFormula feedbackFormula, int id)
        {


            var currentRecipe = feedbackFomulaServices.FindFBFormula(id);



            if (currentRecipe.FeedbackStatus == 1)
            {
                currentRecipe.FeedbackStatus = 1;
            }
            else 
            {
                currentRecipe.FeedbackStatus = 0;
            }
            currentRecipe.Created = DateTime.Now;
            feedbackFomulaServices.EditFormula(currentRecipe);

            return RedirectToAction("feedbackfomula");
        }
    }
}

using IceCream.Models;
using IceCream.Models.BigModel;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            
                ViewBag.profile = from a in db.Accounts
                                  where a.AccId == id
                                  select a;
                ViewBag.accountrecipe = (from f in db.Formulas                                       
                                                join a in db.Accounts on f.ForContributors equals a.AccId
                                         where (f.ForContributors
                                                == id)
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
                                                  
                                                    //ConLai = (x.Soluong - y.Soluong)

                                                }).ToList();
                ViewBag.accountfeedback = (from f in db.Formulas
                                                join fb in db.FeedbackFormulas on f.ForId equals fb.FormulaId
                                                join a in db.Accounts on fb.AccId equals a.AccId
                                                where (a.AccId == id)
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
                                                    


                                                }).ToList();

            ViewBag.SLct = db.Formulas.Where(f => f.ForContributors == id && f.ForStatus == 1).Count();
            ViewBag.SLfb = db.FeedbackFormulas.Where(fb => fb.AccId == id).Count();
            //ViewBag.donvi = db.Formulas.GroupBy(s => s.MaDV)
            //    .Select(g => new {
            //        g.FirstOrDefault().TenDV,
            //        g.FirstOrDefault().NHANVIENs.Count
            //    });


            return View("profile");
        }
        [HttpGet]
        [Route("editprofile/{id}")]
        public IActionResult EditProfile(int id)
        {
            return View("profile", profileService.FindByIdProfile(id));
        }
        [HttpPost]
        [Route("editprofile/{id}")]

        public IActionResult EditProfile(Account account, IFormFile fileavatar)
        {
            //if (HttpContext.Session.GetInt32("account") == id)
            //{
            var currentProfile = profileService.FindByIdProfile(account.AccId)
            ;
            Debug.WriteLine("ID" + account.AccId);

            currentProfile.AccId = account.AccId;
                if (fileavatar != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var ext = fileavatar.ContentType.Split(new char[] { '/' })[1];
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "img/avatar", fileName + "." + ext);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        fileavatar.CopyTo(fileStream);
                    }
                account.AccAvatar = fileName + "." + ext;

                }
                else
                {
                account.AccAvatar = "avatardefault.jpg";
                }
                
                profileService.EditAcccount(account);
                return RedirectToAction("profile");
                
         }
        
            //else
            //{
            //    return View("~/Views/Home/Page404.cshtml");
            //}
            
        
     }
}

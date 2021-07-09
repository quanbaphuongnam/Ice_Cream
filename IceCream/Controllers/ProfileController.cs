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
        private AccountService account;
        private ProfileService profileService;
        private ServiceAccountService serviceAccountService;
        private IWebHostEnvironment webHostEnvironment;
        public ProfileController(DatabaseContext _db, AccountService _account, ServiceAccountService _serviceAccountService, ProfileService _profileService, IWebHostEnvironment _webHostEnvironment)
        {
           
            db = _db;
            profileService = _profileService;
            serviceAccountService = _serviceAccountService;
            webHostEnvironment = _webHostEnvironment;
            account = _account;
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
            ServiceAccount serviceAccount = serviceAccountService.FindAccId(id);
            DateTime accEnd = DateTime.Parse(serviceAccount.SerAccEnd.ToString());
            TimeSpan date = accEnd.Subtract(DateTime.Now);
            Debug.WriteLine("aaaaaaaaaaaa: " + accEnd);
            Debug.WriteLine("aaaaaaaaaaaa: " + DateTime.Now);
            int days = date.Days;
             ViewBag.timeend = days.ToString();
            return View("profile");
        }
        [HttpGet]
        [Route("editprofile")]
        public IActionResult EditProfile(int id)
        {
            return View("profile", profileService.FindByIdProfile(id));
        }
        [HttpPost]
        [Route("editprofile")]

        public IActionResult EditProfile(Account account, IFormFile fileavatar)
        {
            //if (HttpContext.Session.GetInt32("account") == id)
            //{
            var currentProfile = profileService.FindByIdProfile(account.AccId);

         
                if (fileavatar != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var ext = fileavatar.ContentType.Split(new char[] { '/' })[1];
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "img/avatar", fileName + "." + ext);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        fileavatar.CopyTo(fileStream);
                    }
                    currentProfile.AccAvatar = fileName + "." + ext;

                }
                else if(account.AccAvatar != null)
                 {
                    currentProfile.AccAvatar = account.AccAvatar;
              
                }else
                 {
                    currentProfile.AccAvatar = "avatardefault.jpg";
                 }
               
                if (account.AccDob != null) { currentProfile.AccDob = account.AccDob; }
                if (account.AccEmail != null) { currentProfile.AccEmail = account.AccEmail; }
                if (account.AccGender != null) {  currentProfile.AccGender = account.AccGender; }
                if (account.AccPhone != null) { currentProfile.AccPhone = account.AccPhone; }
                if (account.AccAddr != null) { currentProfile.AccAddr = account.AccAddr; }
                if (account.AccFullname != null) { currentProfile.AccFullname = account.AccFullname; }
                profileService.EditAcccount(currentProfile);
                return RedirectToAction("profile", new {id = account.AccId }  );
                
         }
        
            //else
            //{
            //    return View("~/Views/Home/Page404.cshtml");
            //}
            
        
     }
}

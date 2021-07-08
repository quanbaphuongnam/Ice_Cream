using IceCream.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Areas.Admin.Controllers
{
   
    [Route("homeadmin")]
    public class HomeAdminController : Controller
    {
        public DatabaseContext db;
        private IWebHostEnvironment webHostEnvironment;
        public HomeAdminController(DatabaseContext _db,IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
      
            db = _db;
        }

        [Route("homeadmin")]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("admin") != null)
            {
                //So luong cong thức
                ViewBag.SLctadmin = db.Formulas.Count();
                // coong thức chưa duyệt
                ViewBag.SLctnotacadmin = db.Formulas.Where(f => f.ForStatus == 0).Count();
                //tài khoản 
                ViewBag.SLaccountadmin = db.Accounts.Count();
                //hóa đơn
                ViewBag.SLhdadmin = db.InvoiceAccounts.Count();
                return View("homeadmin");
            }
            return View("~/Views/Home/Page404.cshtml");
        }
    }
}

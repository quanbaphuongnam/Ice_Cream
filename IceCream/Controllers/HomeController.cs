using Amazon.Runtime.Internal;
using IceCream.Models;
using IceCream.Models.BigModel;
using IceCream.Paypal;
using IceCream.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Areas.Admin.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private AccountService account;
        private ServiceAccountService serviceAccountService;
        private RecipeService recipeService;
        private IConfiguration configuration;
        public DatabaseContext db;
        public HomeController(AccountService _account,RecipeService _recipeService, IConfiguration _configuration, DatabaseContext _db, ServiceAccountService _serviceAccountService)
        {
            account = _account;
            serviceAccountService = _serviceAccountService;
            recipeService = _recipeService;
            configuration = _configuration;
            db = _db;
        }
        [Route("index")]
        [Route("")]
        [Route("~/")]
        public IActionResult Index()
        {
            ViewBag.postUrl = configuration["PayPal:PostUrl"];
            ViewBag.business = configuration["PayPal:Business"];
            ViewBag.returnUrl = configuration["PayPal:ReturnUrl"];

            ViewBag.top1recipe = (from Formula in db.Formulas
                                  group new { Formula, Formula.ForContributorsNavigation } by new
                                  {
                                      Formula.ForContributors,
                                      Formula.ForContributorsNavigation.AccUsername,
                                      Formula.ForContributorsNavigation.AccAvatar
                                  } into g
                                  select new
                                  {
                                      g.Key.AccUsername,
                                      g.Key.AccAvatar,
                                      g.Key.ForContributors,
                                      Quantity = g.Count(p => p.Formula.ForContributors != null)
                                  });

            return View();
        }

        [Route("signupsuccess")]
        public IActionResult Success([FromQuery(Name = "tx")] string tx)
        {
            var result = PDTHolder.Success(tx, configuration, Request);
            Debug.WriteLine("Customer info:");
            Debug.WriteLine("First Name: " + result.PayerFirstName);
            Debug.WriteLine("LastName: " + result.PayerLastName);
            Debug.WriteLine("Email: " + result.PayerEmail);

            Account acc = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("acc"));
            acc.AccPassword = BCrypt.Net.BCrypt.HashString(acc.AccPassword);
            account.Create(acc);
            Account acc2 = account.Find(acc.AccUsername);
            HttpContext.Session.Remove("acc");
            if (result.GrossTotal == 15)
            {
                ServiceAccount serviceAccount = new ServiceAccount
                {
                    SerId = 1,
                    AccId = acc2.AccId,
                    SerAccCreated = DateTime.Now,
                    SerAccEnd = DateTime.Now.AddDays(30),
                    SerAccPrice = 15,
                };
                serviceAccountService.Create(serviceAccount);
            }
            else
            {
                ServiceAccount serviceAccount = new ServiceAccount
                {
                    SerId = 2,
                    AccId = acc2.AccId,
                    SerAccCreated = DateTime.Now,
                    SerAccEnd = DateTime.Now.AddDays(365),
                    SerAccPrice = 150,
                };
                serviceAccountService.Create(serviceAccount);
            }
            return View("signupsuccess");
        }

        [Route("signupsuccess2")]
        public IActionResult Success2([FromQuery(Name = "tx")] string tx)
        {
            var result = PDTHolder.Success(tx, configuration, Request);
            Debug.WriteLine("Customer info:");
            Debug.WriteLine("First Name: " + result.PayerFirstName);
            Debug.WriteLine("LastName: " + result.PayerLastName);
            Debug.WriteLine("Email: " + result.PayerEmail);

            Account acc = account.Find(HttpContext.Session.GetString("username"));
            acc.AccStatus = 1;
            account.EditAcccount(acc);
            return View("signupsuccess");
        }

        [Route("paypal")]
        public IActionResult Paypal()
        {
            ViewBag.postUrl = configuration["PayPal:PostUrl"];
            ViewBag.business = configuration["PayPal:Business"];
            ViewBag.returnUrl = configuration["PayPal:ReturnUrl"];
            ViewBag.services = db.Services.ToList();
            return View("Paypal");
        }

        [Route("paypal2")]
        public IActionResult Paypal2()
        {
            ViewBag.postUrl = configuration["PayPal:PostUrl"];
            ViewBag.business = configuration["PayPal:Business"];
            ViewBag.returnUrl = configuration["PayPal:ReturnUrl2"];
            ViewBag.services = db.Services.ToList();
            return View("Paypal2");
        }
    }
}

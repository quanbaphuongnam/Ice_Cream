using Amazon.Runtime.Internal;
using IceCream.Models;
using IceCream.Paypal;
using IceCream.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private RecipeService recipeService;
        private IConfiguration configuration;
        public HomeController(AccountService _account,RecipeService _recipeService, IConfiguration _configuration)
        {
            account = _account;
            recipeService = _recipeService;
            configuration = _configuration;
        }
        [Route("index")]
        [Route("")]
        [Route("~/")]
     
        public IActionResult Index()
        {
        
            ViewBag.postUrl = configuration["PayPal:PostUrl"];
            ViewBag.business = configuration["PayPal:Business"];
            ViewBag.returnUrl = configuration["PayPal:ReturnUrl"];
            return View();
        }
        [Route("signupsuccess")]
        public IActionResult Success([FromQuery(Name = "tx")] string tx,Account acc)
        {
            var result = PDTHolder.Success(tx, configuration, Request);
            Debug.WriteLine("Customer info:");
            Debug.WriteLine("First Name: " + result.PayerFirstName);
            Debug.WriteLine("LastName: " + result.PayerLastName);
            Debug.WriteLine("Email: " + result.PayerEmail);
            HttpContext.Session.SetString("result", result.PayerEmail);
            if (HttpContext.Session.GetString("result") != null)
            {

                acc.AccPassword = BCrypt.Net.BCrypt.HashString(acc.AccPassword);
                account.Create(acc);
                HttpContext.Session.Remove("result");
            }
            else
            {
                Debug.WriteLine("ko thành công");
            }
            return View("signupsuccess");
        }

    }
}

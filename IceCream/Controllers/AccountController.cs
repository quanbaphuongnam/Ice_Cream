using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private AccountService account;

        private IWebHostEnvironment webHostEnvironment;
        private IConfiguration configuration;

        public AccountController(AccountService _account, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration)
        {
            account = _account;
            webHostEnvironment = _webHostEnvironment;
            configuration = _configuration;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.postUrl = configuration["PayPal:PostUrl"];
            ViewBag.business = configuration["PayPal:Business"];
            ViewBag.returnUrl = configuration["PayPal:ReturnUrl"];
            return View();
        }
      
        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return RedirectToAction("index", "home", new Account());
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Account acc)
        {
           
            acc.AccPassword = BCrypt.Net.BCrypt.HashString(acc.AccPassword);
            account.Create(acc);
            return RedirectToAction("index", "home", new Account());
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return RedirectToAction("index","home");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            if(!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                if (account.Login(username, password) != null)
                {
                    HttpContext.Session.SetInt32("account", account.Login(username, password).AccId);
                    HttpContext.Session.SetString("username", account.Login(username, password).AccUsername);
                    HttpContext.Session.SetString("msg", "s");
                    //HttpContext.Session.Remove("msg");
                    return RedirectToAction("index","home");
                }
                else
                {
                    HttpContext.Session.SetString("msg", "f");
                }
            }
            return RedirectToAction("index","home");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "home");
        }
    }
}

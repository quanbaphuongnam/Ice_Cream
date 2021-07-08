using IceCream.Helpers;
using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private AccountService account;
        private ServiceAccountService serviceAccountService;
        private IWebHostEnvironment webHostEnvironment;
        private IConfiguration configuration;

        public AccountController(AccountService _account, ServiceAccountService _serviceAccountService, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration)
        {
            account = _account;
            serviceAccountService = _serviceAccountService;
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
            if(account.Find(acc.AccUsername) == null)
            {
                HttpContext.Session.SetString("acc", JsonConvert.SerializeObject(acc));
                return RedirectToAction("paypal", "home");
            }
            else
            {
                HttpContext.Session.SetString("msgSignUp", "f");
            }
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
                Account acc = account.Login(username, password);
                if (acc != null)
                {
                    ServiceAccount serviceAccount = serviceAccountService.FindAccId(acc.AccId);
                    DateTime accEnd = DateTime.Parse(serviceAccount.SerAccEnd.ToString());
                    TimeSpan date = accEnd.Subtract(DateTime.Now);
                    Debug.WriteLine("aaaaaaaaaaaa: " + accEnd);
                    Debug.WriteLine("aaaaaaaaaaaa: " + DateTime.Now);
                    int days = date.Days;
                    if(int.Parse(days.ToString()) <= 0)
                    {
                        acc.AccStatus = 0;
                        account.EditAcccount(acc);
                    }
                    Debug.WriteLine("aaaaaaaaaaaa: " + days.ToString());
                    int status = int.Parse(account.Find(username).AccStatus.ToString());
                    if (status == 1)
                    {
                        HttpContext.Session.SetInt32("account", account.Login(username, password).AccId);
                        HttpContext.Session.SetString("username", account.Login(username, password).AccUsername);
                        HttpContext.Session.SetString("msg", "s");
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        HttpContext.Session.SetString("userSignUp", account.Login(username, password).AccUsername);
                        HttpContext.Session.SetString("msg", "e");
                        return RedirectToAction("paypal2", "home");
                    }
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

        [HttpGet]
        [Route("forgotpassword")]
        public IActionResult forgotpassword()
        {
            return View("Forgotpassword");
        }
        [HttpPost]
        [Route("forgotpassword")]
        public IActionResult forgotpassword(string username, string email)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(email))
            {
                if (account.checkemail(username, email) != null)
                {
                    string content = "Name: " + username;
                    int num = new Random().Next(1000, 9999);
                    content += "<br><br>Confirmation Code: " + num;
                    if (EmailHelpers.Send("IceCreamAd123@gmail.com", email, "Email confirmation code", content))
                    {
                        Debug.WriteLine(num);
                        HttpContext.Session.SetString("userCheck", username);
                        HttpContext.Session.SetInt32("msgnum", num);
                        HttpContext.Session.SetString("msgforgotpassword", "s");
                        return RedirectToAction("Forgotpassword");
                    }
                    else
                    {
                        HttpContext.Session.SetString("msgforgotpassword", "f");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("msgforgotpassword", "f");
                }
            }
            return RedirectToAction("Forgotpassword");
        }
        [HttpPost]
        [Route("checkVerification")]
        public IActionResult CheckVerification(int code)
        {
                if (HttpContext.Session.GetInt32("msgnum") == code)
                {
                    HttpContext.Session.SetString("msgcheckcode", "s");
                    HttpContext.Session.Remove("msgforgotpassword");
                }
                else
                {
                    HttpContext.Session.SetString("msgcheckcode", "f");
                }
            return RedirectToAction("Forgotpassword");
        }
        [HttpPost]
        [Route("updatePassword")]
        public IActionResult updatePassword(string cfpass)
        {
            if (!String.IsNullOrEmpty(cfpass))
            {
                Account acc = account.Find(HttpContext.Session.GetString("userCheck"));
                acc.AccPassword = BCrypt.Net.BCrypt.HashString(cfpass);
                account.EditAcccount(acc);
                HttpContext.Session.SetString("msgNewPass", "s");
            }
            return RedirectToAction("index", "home");
        }
    }
}

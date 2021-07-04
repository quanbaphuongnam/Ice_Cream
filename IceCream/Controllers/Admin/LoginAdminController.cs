using Castle.Core.Configuration;
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
    [Route("loginAdmin")]
    public class LoginAdminController : Controller
    {
        private AccountService account;

        public LoginAdminController(AccountService _account)
        {
            account = _account;
        }

        [Route("login")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        public ActionResult Login(string username, string password)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {

                if (account.Login(username, password) != null)
                {
                    int role = int.Parse(account.Login(username, password).AccRole.ToString());
                    if (role == 2)
                    {
                        HttpContext.Session.SetInt32("admin", account.Login(username, password).AccId);
                        HttpContext.Session.SetString("msg", "s");
                        return RedirectToAction("homeadmin", "homeadmin");
                    }
                    HttpContext.Session.SetString("msg", "f");
                }
                else
                {
                    HttpContext.Session.SetString("msg", "f");
                }
            }
            return RedirectToAction("login", "loginAdmin");
        }
    }
}

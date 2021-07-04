using IceCream.Helpers;
using IceCream.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        [Route("contact")]
        [Route("")]
        public IActionResult Index()
        {
            return View("Contact");
        }

        [HttpPost]
        [Route("send")]
        public IActionResult Send(Contact contact)
        {
            string content = "Name: " + contact.Name;
            content += "<br><br>Message: " + contact.Message;
            content += "<br>Email: " + contact.Email;
            if (EmailHelpers.Send(contact.Email, "IceCreamAd123@gmail.com", contact.Subject, content))
            {
                HttpContext.Session.SetString("msgContact", "t");
                return RedirectToAction("contact");
            }
            else
            {
                HttpContext.Session.SetString("msgContact", "f");
                return RedirectToAction("contact");
            }
        }
    }
}

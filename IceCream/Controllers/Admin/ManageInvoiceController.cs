using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Controllers.Admin
{
    [Route("manageinvoice")]
    public class ManageInvoiceController : Controller
    {
        public DatabaseContext db;
       
        private ManageInvoiceServices manageInvoiceServices;
        private InvoiceDetailAccountService invoiceDetailAccountService;

        public ManageInvoiceController(DatabaseContext _db, ManageInvoiceServices _manageInvoiceServices, InvoiceDetailAccountService _invoiceDetailAccountService)
        {
            db = _db;

            manageInvoiceServices = _manageInvoiceServices;
            invoiceDetailAccountService = _invoiceDetailAccountService;
        }

        [Route("manageinvoice")]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.manageInvoice = manageInvoiceServices.FindAll();
            return View("manageinvoice");
        }
        [Route("detail")]
        public IActionResult Detail(string id)
        {

            ViewBag.detail = from a in db.InvoiceDetailAccounts
                              where a.InvId == id
                              select a;
            return View("detail");
        }

    }
}

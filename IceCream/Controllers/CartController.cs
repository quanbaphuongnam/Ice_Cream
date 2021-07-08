using IceCream.Models;
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

namespace IceCream.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        public DatabaseContext db;
        private BookService bookService;
        private InvoiceService invoiceService;
        private InvoiceDetailAccountService invoiceDetailAccountService;
        private IConfiguration configuration;
        public CartController(DatabaseContext _db, BookService _bookService, IConfiguration _configuration, InvoiceService _invoiceService, InvoiceDetailAccountService _invoiceDetailAccountService)
        {
            db = _db;
            bookService = _bookService;
            configuration = _configuration;
            invoiceService = _invoiceService;
            invoiceDetailAccountService = _invoiceDetailAccountService;
        }

        [Route("cart")]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("cart") != null)
            {
                List<Cart> cart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
                ViewBag.cart = cart;
            }
            return View("Cart");
        }

        [Route("checkout")]
        public IActionResult CheckOut()
        {
            if (HttpContext.Session.GetString("cart") != null)
            {
                List<Cart> cart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
                ViewBag.cart = cart;
            }
            else
            {
                return View("~/Views/Home/Page404.cshtml");
            }
            ViewBag.postUrl = configuration["PayPal:PostUrl"];
            ViewBag.business = configuration["PayPal:Business"];
            ViewBag.returnUrl = configuration["PayPal:ReturnUrl3"];
            return View("checkout");
        }

        [Route("billingDetails")]
        public IActionResult BillingDetails(InvoiceAccount invoiceAccount)
        {
            Debug.WriteLine("aaaaa: " + invoiceAccount.Email);
            Debug.WriteLine("aaaaa: " + invoiceAccount.Addr);
            HttpContext.Session.SetString("billing", JsonConvert.SerializeObject(invoiceAccount));
            return RedirectToAction("checkout");
        }


        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Cart> cart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
            int index = Exists(id, cart);
            cart.RemoveAt(index);
            if (cart.Count() == 0)
            {
                HttpContext.Session.Remove("cart");
                return View("Cart");
            }
            else
            {
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
                HttpContext.Session.SetInt32("jsoncart", cart.Count());
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(int[] quantity)
        {
            List<Cart> cart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
            for (var i = 0; i < cart.Count; i++)
            {
                cart[i].quantity = quantity[i];
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index");
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            var book = bookService.Find(id);
            if (HttpContext.Session.GetString("cart") == null)
            {
                var cart = new List<Cart>
                {
                    new Cart
                    {
                        book = book,
                        quantity = 1
                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
                Debug.WriteLine(book.BookId);
                HttpContext.Session.SetInt32("jsoncart", cart.Count());
            }
            else
            {
                List<Cart> cart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
                int index = Exists(id, cart);
                if (index == -1)
                {
                    cart.Add(new Cart
                    {
                        book = book,
                        quantity = 1
                    });
                }
                else
                {
                    cart[index].quantity++;
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
                HttpContext.Session.SetInt32("jsoncart", cart.Count());
            }
            return RedirectToAction("index");
        }

        private int Exists(int id, List<Cart> items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].book.BookId == id)
                {
                    return i;
                }
            }
            return -1;
        }

        [Route("checkoutsuccess")]
        public IActionResult Success([FromQuery(Name = "tx")] string tx)
        {
            var result = PDTHolder.Success(tx, configuration, Request);
            InvoiceAccount invoiceAccount = JsonConvert.DeserializeObject<InvoiceAccount>(HttpContext.Session.GetString("billing"));
            InvoiceAccount invoice = new InvoiceAccount
            {
                InvId = result.TransactionId,
                AccId = (HttpContext.Session.GetInt32("account")!=null)? HttpContext.Session.GetInt32("account"): null,
                Name = invoiceAccount.Name,
                Email = invoiceAccount.Email,
                Addr = invoiceAccount.Addr,
                Phone = invoiceAccount.Phone,
                InvCreated = DateTime.Now,
                InvTotal = int.Parse(result.GrossTotal.ToString()),
                InvPayment = "Paypal",
                InvStatus = "Paid",
            };
            invoiceService.Create(invoice);
            List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
            foreach (var cart in carts)
            {
                InvoiceDetailAccount invoiceDetail = new InvoiceDetailAccount
                {
                    InvId = result.TransactionId,
                    BookId = cart.book.BookId,
                    ScName = cart.book.BookName,
                    ScQuantity = cart.quantity,
                    ScPhoto = cart.book.BookPhoto,
                    ScPrice = cart.book.BookPrice,
                    Total = cart.quantity * cart.book.BookPrice,
                };
                invoiceDetailAccountService.Create(invoiceDetail);
                Book book = bookService.Find(cart.book.BookId);
                book.BookQuantity -= cart.quantity;
                bookService.Edit(book);
            };
            HttpContext.Session.SetString("msgbilling", "s");
            HttpContext.Session.Remove("billing");
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Remove("jsoncart");
            return RedirectToAction("allbook", "book");
        }
    }
}

using IceCream.Models;
using IceCream.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CartController(DatabaseContext _db, BookService _bookService)
        {
            db = _db;
            bookService = _bookService;
        }

        [Route("index")]
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
            return View("checkout");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Cart> cart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
            int index = Exists(id, cart);
            cart.RemoveAt(index);
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            HttpContext.Session.SetInt32("jsoncart", cart.Count());
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
    }
}

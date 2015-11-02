using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SportsStore.LCW.Domain.Abstract;
using SportsStore.LCW.Domain.Entities;
using SportsStore.LCW.WebUI.Models;

namespace SportsStore.LCW.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductsRepository repo,IOrderProcessor proc) {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Checkout()
        {

            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) {
            if (!cart.Lines.Any())
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (ModelState.IsValid) {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.ClearItem();
                return View("Completed");
            }

            return View(shippingDetails);
        }

        public ActionResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int productId, string returnUrl) {
            var product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId, string returnUrl) {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null) {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public PartialViewResult Summary(Cart cart) {
            return PartialView(cart);
        }

        //获取购物车session
        //private Cart GetCart() {
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null) {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}
    }
}

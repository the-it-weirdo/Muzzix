using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineMusicStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace OnlineMusicStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IOrderRepository _orderRepository;
        private readonly Cart _Cart;

        public OrderController(IOrderRepository orderRepository, Cart Cart)
        {
            _orderRepository = orderRepository;
            _Cart = Cart;
        }


       public IActionResult CheckOut()
        {
            _Cart.ClearCart();
            return View();
        }

       /* [Authorize]
        [HttpPost]
        public IActionResult CheckOut(Order Order)
        {
           

            var items = _Cart.GetCartItems();

            if (_Cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some Music first");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(Order);
                _Cart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(Order);

        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Hey there, thanks for your order and enjoy your music!";
            return View();
        }
        */

       }
}

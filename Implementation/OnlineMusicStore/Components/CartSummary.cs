using OnlineMusicStore.Models;
using OnlineMusicStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineMusicStore.Components
{
    public class CartSummary : ViewComponent
    {

        private readonly Cart _Cart;

        public CartSummary(Cart Cart)
        {
            _Cart = Cart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _Cart.GetCartItems();
            // var items = new List<ShoppingCartItem>() { new ShoppingCartItem(), new ShoppingCartItem() };
            _Cart.CartItems = items;

            var CartViewModel = new CartViewModel
            {
                Cart = _Cart,
                CartTotal = _Cart.GetCartTotal()
            };
            return View(CartViewModel);
        }
    }
}

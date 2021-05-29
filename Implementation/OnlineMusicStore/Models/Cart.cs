using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineMusicStore.Data;


namespace OnlineMusicStore.Models
{
    public class Cart
    {
        private readonly ApplicationDbContext _appDbContext;

        public Cart(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }

       public static Cart GetCart(IServiceProvider services)
        {
            // Use GetRequiredService where you require the service. It will throw an exception,
            // when the service is not registered.
            // GetService on the other side is for optional dependencies, which will just return
            // null when there is no such service registered.

            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new Cart(context) { CartId = cartId };
        } 

        public void AddToCart(Music m, int amount)
        {
            var CartItem =
                _appDbContext.CartItems.SingleOrDefault(
                    s => s.Music.Id == m.Id && s.CartId == CartId);

            if (CartItem == null)
            {
                CartItem = new CartItem
                {
                    CartId = CartId,
                    Music = m,
                    Amount = 1
                };

                _appDbContext.CartItems.Add(CartItem);
            }
            else
            {
                CartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public double RemoveFromCart(Music m)
        {
            var CartItem =
                _appDbContext.CartItems.SingleOrDefault(
                    s => s.Music.Id == m.Id && s.CartId == CartId);

            double localAmount = 0;

            if (CartItem != null)
            {
                if (CartItem.Amount > 0)
                {
                    CartItem.Amount--;
                    localAmount = CartItem.Amount;
                }
                else
                {
                    _appDbContext.CartItems.Remove(CartItem);
                }
            }

            _appDbContext.SaveChanges();
            return localAmount;
        }

        public List<CartItem> GetCartItems()
        {
          var myList = _appDbContext.CartItems.Where(c => c.CartId == CartId)
                     .Include(a => a.Music).ToList();

            return myList;
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .CartItems
                .Where(cart => cart.CartId == CartId);

            _appDbContext.CartItems.RemoveRange(cartItems);
            _appDbContext.SaveChanges();
        }

        public double GetCartTotal()
        {
            var total = _appDbContext.CartItems.Where(c => c.CartId == CartId)
                .Select(c => c.Music.Price * c.Amount).Sum();
            return total;
        }

   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OnlineMusicStore.Data;

namespace OnlineMusicStore.Models
{
    
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly Cart _Cart;

        public OrderRepository(ApplicationDbContext appDbContext, Cart Cart)
        {
            _appDbContext = appDbContext;
            _Cart = Cart;
        }
      
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _appDbContext.Orders.Add(order);

            var CartItems = _Cart.CartItems;
            double orderTotal = 0;

            foreach (var CartItem in CartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = CartItem.Amount,
                    MusicId = CartItem.Music.Id,
                    Price = CartItem.Music.Price,
                    OrderId = order.OrderId
                };

                orderTotal = CartItem.Amount * CartItem.Music.Price;
                _appDbContext.OrderDetails.Add(orderDetail);
            }

            order.OrderTotal += orderTotal;
            _appDbContext.SaveChanges();
        }

    }
}

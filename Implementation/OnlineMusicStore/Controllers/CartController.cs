using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using OnlineMusicStore.Models;
using OnlineMusicStore.ViewModels;
using OnlineMusicStore.Data;

namespace OnlineMusicStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly Cart _Cart;

        public CartController(ILogger<CartController> logger, ApplicationDbContext context, Cart cart)
        {
            _logger = logger;
            _dbContext = context;
            _Cart = cart;
        }

        public IActionResult Index()
        {
            var items = _Cart.GetCartItems();
            _Cart.CartItems = items;

            var CartViewModel = new CartViewModel
            {
                Cart = _Cart,
                CartTotal = _Cart.GetCartTotal()
            };

            return View(CartViewModel);
        }

      

        public IActionResult AddMusicToCart(int? musicId)
        {
            if (musicId == null)
                return NotFound();

            var music = _dbContext.Musics.FirstOrDefault(m => m.Id == musicId);

            if (music == null)
                return NotFound();

            if (music != null)
            {
                _Cart.AddToCart(music, 1);
            }

            return RedirectToAction("Index");
            //return View("Index", new List<Music>() { music });
        }

        public IActionResult AddAlbumToCart(int? albumId)
        {
            if(albumId == null)
                return NotFound();

           var musics = _dbContext.Musics.ToList().Where(m => m.AlbumId == albumId).ToList();

          foreach(var music in musics) {
                _Cart.AddToCart(music, 1);
            }

          return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int musicId)
        {
            var music = _dbContext.Musics.FirstOrDefault(m => m.Id == musicId);
            if (music != null)
            {
                _Cart.RemoveFromCart(music);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            var cartItems = _dbContext
                .CartItems
                .Where(cart => cart.CartId == _Cart.CartId);

            _dbContext.CartItems.RemoveRange(cartItems);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
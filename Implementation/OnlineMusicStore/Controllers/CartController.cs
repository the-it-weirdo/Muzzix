using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using OnlineMusicStore.Models;
using OnlineMusicStore.Data;

namespace OnlineMusicStore.Controllers
{
    [Authorize]    
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public CartController(ILogger<CartController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Demo function. Can be removed
        public IActionResult AddMusicToCart(int? musicId)
        {
            if (musicId == null)
                return NotFound();

            var music = _dbContext.Musics.FirstOrDefault(m => m.Id == musicId);

            if (music == null)
                return NotFound();

            return View("Index", new List<Music>() { music });
        }

        // Demo function. Can be removed.
        public IActionResult AddAlbumToCart(int? albumId)
        {
            if(albumId == null)
                return NotFound();

            var musics = _dbContext.Musics.ToList().Where(m => m.AlbumId == albumId).ToList();

            return View("Index", musics);
        }
    }
}
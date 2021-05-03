using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMusicStore.Data;
using OnlineMusicStore.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            // var recentMusicUrls = (from music in _dbContext.Musics orderby music.DateAdded select music).Take(3);

            var recentMusicUrls = _dbContext.Musics
            .Include(m => m.Artists)
            .Include(m => m.Album)
            .Include(m => m.Genre)
            .OrderByDescending(m => m.DateAdded)
            .Take(3);

            _logger.LogInformation($"In Index: {recentMusicUrls.Count()}");
            return View(recentMusicUrls.ToList());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

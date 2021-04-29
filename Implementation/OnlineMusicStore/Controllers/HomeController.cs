using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMusicStore.Data;
using OnlineMusicStore.Models;

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
            var recentMusicUrls = _dbContext.Musics
            .ToList()
            .Where(m => m.IsRecentlyAdded() && m.IsRecentlyReleased())
            .ToList();

            _logger.LogInformation($"In Index: {recentMusicUrls.Count}");
            return View(recentMusicUrls);
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

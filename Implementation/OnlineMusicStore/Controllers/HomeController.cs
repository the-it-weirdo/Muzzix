using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMusicStore.Data;
using OnlineMusicStore.Models;
using OnlineMusicStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

            if (User.IsInRole(UserRoles.AdminRole))
                return View("IndexAdmin");

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
            return View(new ContactUsViewModel()
            {
                FeedbackViewModel = new FeedbackViewModel(),
                RequestViewModel = new RequestViewModel()
            });
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public IActionResult ShowFeedbacks()
        {
            var feedbacks = _dbContext.Feedbacks.ToList();
            ViewData["Type"] = ContactFormType.FEEDBACK;
            return View("ShowRequestsAndFeedbacks", feedbacks);
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public IActionResult ShowRequests()
        {
            var requests = _dbContext.Requests.ToList();
            ViewData["Type"] = ContactFormType.REQUEST;
            return View("ShowRequestsAndFeedbacks", requests);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (!ModelState.IsValid)
                return View("Contact", feedbackViewModel);

            var feedback = new Feedback()
            {
                Name = feedbackViewModel.Name,
                Email = feedbackViewModel.Email,
                Rating = feedbackViewModel.Rating,
                FeedbackMessage = feedbackViewModel.FeedbackMessage
            };

            _dbContext.Feedbacks.Add(feedback);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Contact", "Home");
        }

        public async Task<IActionResult> SubmitRequest(RequestViewModel requestViewModel)
        {
            if (!ModelState.IsValid)
                return View("Contact", requestViewModel);

            var request = new Request()
            {
                Name = requestViewModel.Name,
                Email = requestViewModel.Email,
                RequestType = requestViewModel.RequestType,
                RequestMessage = requestViewModel.RequestMessage
            };

            _dbContext.Requests.Add(request);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Contact", "Home");
        }

        public IActionResult Abstract()
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

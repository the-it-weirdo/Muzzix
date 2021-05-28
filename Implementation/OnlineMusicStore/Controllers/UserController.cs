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
    [Authorize(Roles = UserRoles.AdminRole)]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            return View(_dbContext.AllUsers.ToList());
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _dbContext.AllUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();
            _dbContext.AllUsers.Remove(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

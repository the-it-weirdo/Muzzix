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
    public class GenreController : Controller
    {
        private readonly ILogger<GenreController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public GenreController(ILogger<GenreController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var genres = _dbContext.Genres.ToList();
            return View(genres);
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public IActionResult CreateNew()
        {
            return View("GenreForm", new GenreFormViewModel());
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            
            var genre = await _dbContext.Genres.FindAsync(id);
            
            if (genre == null)
                return NotFound();

            return View("GenreForm", new GenreFormViewModel(genre));
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            
            var genre = await _dbContext.Genres.FindAsync(id);
            
            if (genre == null)
                return NotFound();
            else
                _dbContext.Genres.Remove(genre);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.AdminRole)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Genre genre)
        {

            if (!ModelState.IsValid)
                return View("GenreForm", genre);

            if (genre.Id == 0)
                _dbContext.Genres.Add(genre);
            else
            {
                var genreFromDb = _dbContext.Genres.Single(ar => ar.Id == genre.Id);

                genreFromDb.Name = genre.Name;
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
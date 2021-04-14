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
    public class ArtistController : Controller
    {
        private readonly ILogger<ArtistController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public ArtistController(ILogger<ArtistController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var artists = _dbContext.Artists.ToList();
            return View(artists);
        }

        public IActionResult CreateNew()
        {
            return View("ArtistForm", new ArtistFormViewModel());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var artist = await _dbContext.Artists.FindAsync(id);

            if (artist == null)
                return NotFound();

            return View("ArtistForm", new ArtistFormViewModel(artist));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var artist = await _dbContext.Artists.FindAsync(id);

            if (artist == null)
                return NotFound();
            else
                _dbContext.Artists.Remove(artist);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Artist artist)
        {

            if (!ModelState.IsValid)
                return View("ArtistForm", artist);

            if (artist.Id == 0)
                _dbContext.Artists.Add(artist);
            else
            {
                var artitstFromDb = _dbContext.Artists.Single(ar => ar.Id == artist.Id);

                artitstFromDb.Name = artist.Name;
                artitstFromDb.Rating = artist.Rating;
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using OnlineMusicStore.Models;
using OnlineMusicStore.ViewModels;
using OnlineMusicStore.Data;

namespace OnlineMusicStore.Controllers
{
    [Authorize]
    public class MusicController : Controller
    {
        private readonly ILogger<MusicController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public MusicController(ILogger<MusicController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var musics = _dbContext.Musics.ToList();
            return View(musics);
        }

        public IActionResult CreateNew()
        {
            var viewModel = new MusicFormViewModel();
            viewModel.SetArtists(_dbContext.Artists.ToList());
            viewModel.SetGenres(_dbContext.Genres.ToList());

            return View("MusicForm", viewModel);
        }

        public IActionResult Detail(int? id)
        {
            var music = _dbContext.Musics
            .Include(m => m.Genre)
            .Include(m => m.Album)
            .Include(m => m.Artists)
            .SingleOrDefault(m => m.Id == id);

            if (music == null)
                return NotFound();

            return View(music);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
                
            var music = _dbContext.Musics
            .Include(m => m.Artists)
            .SingleOrDefault(m => m.Id == id);

            if (music == null)
                return NotFound();

            var viewModel = new MusicFormViewModel(music);
            viewModel.SetArtists(_dbContext.Artists.ToList());
            viewModel.SetGenres(_dbContext.Genres.ToList());
            return View("MusicForm", viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var music = await _dbContext.Musics.FindAsync(id);
            
            if (music == null)
                return NotFound();
            else
                _dbContext.Musics.Remove(music);
            
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(MusicFormViewModel musicFormViewModel)
        {

            if (!ModelState.IsValid)
                return View("MusicForm", musicFormViewModel);

            var music = musicFormViewModel.MapToMusicObject();
            foreach (var artistId in musicFormViewModel.SelectedArtistIds)
            {
                var artist = await _dbContext.Artists.SingleOrDefaultAsync(ar => ar.Id == artistId);
                if (artist != null)
                    music.Artists.Add(artist);
                _logger.LogInformation($"Adding {artist.Name} to music {music.Name} with Id {music.Id}");
            }

            if (music.Id == 0)
            {
                music.DateAdded = DateTime.Now;
                _logger.LogInformation($"Creating new music {music.Name} with {music.Artists.Count()} artists.");
                _dbContext.Musics.Add(music);
            }
            else
            {
                _logger.LogInformation($"Updating Music with Id: {music.Id}");
                var musicFromDb = _dbContext.Musics
                .Include(m => m.Artists)
                .SingleOrDefault(m => m.Id == music.Id);

                if (musicFromDb == null)
                    return NotFound();

                musicFromDb.Name = music.Name;
                musicFromDb.Language = music.Language;
                musicFromDb.ImageUrl = music.ImageUrl;
                musicFromDb.GenreId = music.GenreId;
                musicFromDb.DateReleased = music.DateReleased;
                musicFromDb.Artists = music.Artists;

            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
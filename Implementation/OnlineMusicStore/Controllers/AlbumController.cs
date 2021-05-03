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
    public class AlbumController : Controller
    {
        private readonly ILogger<AlbumController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public AlbumController(ILogger<AlbumController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public IActionResult Index()
        {
            var albums = _dbContext.Albums.ToList();
            return View(albums);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var album = _dbContext.Albums
            .Include(a => a.Musics)
            .Include(a => a.Artists)
            .SingleOrDefault(a => a.Id == id);

            // loading nested related entity.
            // album => Music => Genre
            _dbContext.Entry(album)
            .Collection(a => a.Musics).Query()
            .Include(m => m.Genre)
            .Load();

            if (album == null)
                return NotFound();

            return View(album);
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public IActionResult CreateNew()
        {
            var viewModel = new AlbumFormViewModel();
            viewModel.SetMusic(_dbContext.Musics.ToList());
            return View("AlbumForm", viewModel);
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var album = _dbContext.Albums
            .Include(a => a.Musics)
            .SingleOrDefault(a => a.Id == id);

            if (album == null)
                return NotFound();

            var viewModel = new AlbumFormViewModel(album);
            viewModel.SetMusic(_dbContext.Musics.ToList());
            return View("AlbumForm", viewModel);
        }
        
        [Authorize(Roles = UserRoles.AdminRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var album = await _dbContext.Albums.FindAsync(id);

            if (album == null)
                return NotFound();
            else
                _dbContext.Albums.Remove(album);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Authorize(Roles = UserRoles.AdminRole)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(AlbumFormViewModel albumFormViewModel)
        {
            if (!ModelState.IsValid)
                return View("AlbumForm", albumFormViewModel);

            var album = new Album()
            {
                Id = albumFormViewModel.Id.Value,
                Name = albumFormViewModel.Name,
                ImageUrl = albumFormViewModel.ImageUrl,
                Musics = new HashSet<Music>(),
                Artists = new HashSet<Artist>()
            };

            foreach (var musicId in albumFormViewModel.SelectedMusicIds)
            {
                var music = await _dbContext.Musics
                .Include(m => m.Artists)
                .SingleOrDefaultAsync(m => m.Id == musicId);

                if (music == null)
                    _logger.LogInformation($"No music found with Id {musicId}");
                else
                {
                    _logger.LogInformation($"Adding {music.Name} with {music.Artists.Count()} artists.");
                    album.Musics.Add(music);
                    foreach (var artist in music.Artists)
                        album.Artists.Add(artist);
                }
            }

            if (album.Id == 0)
            {
                _logger.LogInformation("Creating New Album with: ");
                _logger.LogInformation($"Musics: {album.Musics.Count()}"
                                    + $"\nArtists: {album.Artists.Count()}");

                _dbContext.Albums.Add(album);
            }
            else
            {
                _logger.LogInformation($"Updating Album with Id: {album.Id}");
                var albumFromDb = await _dbContext.Albums
                .Include(al => al.Artists)
                .Include(al => al.Musics)
                .SingleOrDefaultAsync(a => a.Id == album.Id);

                if (albumFromDb == null)
                    return NotFound();

                albumFromDb.Name = album.Name;
                albumFromDb.ImageUrl = album.ImageUrl;

                // For Musics
                // If in db but not in form data, remove from db
                foreach (var music in albumFromDb.Musics)
                    if (!album.Musics.Contains(music))
                        albumFromDb.Musics.Remove(music);

                // If in form data, but not in db, add to db
                foreach (var music in album.Musics)
                    if (!albumFromDb.Musics.Contains(music))
                        albumFromDb.Musics.Add(music);

                // For Artists
                // If in db but not in form data, remove from db
                foreach (var artist in albumFromDb.Artists)
                    if (!album.Artists.Contains(artist))
                        albumFromDb.Artists.Remove(artist);

                // If in form data, but not in db, add to db
                foreach (var artist in album.Artists)
                    if (!albumFromDb.Artists.Contains(artist))
                        albumFromDb.Artists.Add(artist);

            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
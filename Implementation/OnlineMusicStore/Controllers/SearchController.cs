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
using OnlineMusicStore.Repositories;

namespace OnlineMusicStore.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;


        private readonly SearchRepository _repository;

        public SearchController(ILogger<SearchController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _repository = new SearchRepository(context);
        }

        public IActionResult SearchByLanguage(string language = "")
        {
            _logger.LogInformation(language);

            return View("SearchResults", new SearchViewModel
            {
                QueryString = language == "" ? "All Language" : $"Musics in {language}.",
                MusicsByLanguage = _repository.SearchByLanguage(language)
            });
        }

        public IActionResult SearchByQuery(string queryString)
        {
            var viewModel = new SearchViewModel
            {
                QueryString = queryString,
                Musics = _repository.SearchMusics(queryString),
                MusicsByLanguage = _repository.SearchByLanguage(queryString),
                Albums = _repository.SearchAlbums(queryString),
                Artists = _repository.SearchArtists(queryString),
                Genres = _repository.SearchGenres(queryString)
            };

            return View("SearchResults", viewModel);
        }

        public IActionResult SearchByGenre(string genreName)
        {
            _logger.LogInformation(genreName);

            return View("SearchResults", new SearchViewModel
            {
                QueryString = $"Musics with {genreName} genre.",
                Musics = _repository.SearchByGenre(genreName)
            });
        }

        public IActionResult SearchByArtist(int id)
        {

            var artist = _repository.SearchArtist(id);
            if (artist == null)
                return NotFound();

            return View("SearchResults", new SearchViewModel
            {
                QueryString = $"Works of artist {artist.Name}",
                Musics = _repository.SearchMusicByArtist(artist),
                Albums = _repository.SearchAlbumByArtist(artist)
            });
        }

        public IActionResult SearchDefault()
        {
            var viewModel = new SearchViewModel()
            {
                Musics = _repository.SearchMusics(),
                Albums = _repository.SearchAlbums(),
                Artists = _repository.SearchArtists(),
                Genres = _repository.SearchGenres()
            };
            return View("SearchResults", viewModel);
        }

        public IActionResult SearchAll(string allType)
        {
            List<object> items;
            switch (allType)
            {
                case "Artist":
                    {
                        items = _repository.SearchArtists().ConvertAll(x => (object)x);
                        ViewData["AllType"] = "Artists";
                        break;
                    }
                case "Album":
                    {
                        items = _repository.SearchAlbums().ConvertAll(x => (object)x);
                        ViewData["AllType"] = "Albums";
                        break;
                    }
                case "Music":
                    {
                        items = _repository.SearchMusics().ConvertAll(x => (object)x);
                        ViewData["AllType"] = "Musics";
                        break;
                    }
                case "Genre":
                    {
                        items = _repository.SearchGenres().ConvertAll(x => (object)x);
                        ViewData["AllType"] = "Genres";
                        break;
                    }
                case "Language":
                    {
                        return RedirectToAction("SearchByLanguage", "Search");
                    }
                default:
                    {
                        return RedirectToAction("SearchDefault", "Search");
                    }
            }
            return View(items);
        }
    }
}
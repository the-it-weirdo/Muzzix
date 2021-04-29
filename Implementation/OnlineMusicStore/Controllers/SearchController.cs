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
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public SearchController(ILogger<SearchController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        private List<Album> SearchAlbums(string query = "")
        {
            var albums = _dbContext.Albums.Where(al => al.Name.Contains(query)).ToList();
            return albums;
        }

        private List<Music> SearchMusics(string query = "")
        {
            var musics = _dbContext.Musics.Where(m => m.Name.Contains(query)).ToList();
            return musics;
        }

        private List<Artist> SearchArtists(string query = "")
        {
            var artists = _dbContext.Artists
            .Where(a => a.Name.Contains(query))
            .OrderBy(a => a.Name)
            .ToList();
            return artists;
        }

        private List<Genre> SearchGenres(string query = "")
        {
            var genres = _dbContext.Genres
            .Where(al => al.Name.Contains(query))
            .OrderBy(g => g.Name)
            .ToList();
            return genres;
        }

        private List<Music> SearchByLanguage(string language = "")
        {
            var musics = _dbContext.Musics
            .Where(m => m.Language.Contains(language))
            .OrderBy(m => m.Name)
            .ToList();
            return musics;
        }

        public IActionResult SearchByLanguageAction(string language)
        {
            _logger.LogInformation(language);
            var musics = SearchByLanguage(language);

            return View("SearchResults", new SearchViewModel
            {
                QueryString = $"Musics in {language}.",
                Musics = musics
            });
        }

        public IActionResult SearchByQuery(string queryString)
        {
            var viewModel = new SearchViewModel
            {
                QueryString = queryString,
                Musics = SearchMusics(queryString),
                MusicsByLanguage = SearchByLanguage(queryString),
                Albums = SearchAlbums(queryString),
                Artists = SearchArtists(queryString),
                Genres = SearchGenres(queryString)
            };

            return View("SearchResults", viewModel);
        }

        public IActionResult SearchByGenre(string genreName)
        {
            _logger.LogInformation(genreName);
            var musics = _dbContext.Musics
            .Include(m => m.Genre)
            .Where(m => m.Genre.Name.Contains(genreName))
            .ToList();

            return View("SearchResults", new SearchViewModel
            {
                QueryString = $"Musics with {genreName} genre.",
                Musics = musics
            });
        }

        public IActionResult SearchByArtist(int id)
        {
            var artist = _dbContext.Artists.SingleOrDefault(a => a.Id == id);
            if (artist == null)
                return NotFound();

            var musics = _dbContext.Musics
            .Include(m => m.Artists)
            .Where(m => m.Artists.Contains(artist))
            .ToList();

            var albums = _dbContext.Albums
            .Include(al => al.Artists)
            .Where(al => al.Artists.Contains(artist))
            .ToList();

            return View("SearchResults", new SearchViewModel
            {
                QueryString = $"Works of artist {artist.Name}",
                Musics = musics,
                Albums = albums
            });
        }

        public IActionResult SearchAll(string allType)
        {
            var viewModel = new SearchViewModel();
            viewModel.QueryString = $"All {allType}";
            switch (allType)
            {
                case "Artist":
                    {
                        viewModel.Artists = SearchArtists();
                        break;
                    }
                case "Album":
                    {
                        viewModel.Albums = SearchAlbums();
                        break;
                    }
                case "Music":
                    {
                        viewModel.Musics = SearchMusics();
                        break;
                    }
                case "Genre":
                    {
                        viewModel.Genres = SearchGenres();
                        break;
                    }
                case "Language":
                    {
                        viewModel.MusicsByLanguage = SearchByLanguage();
                        break;
                    }
                default:
                    {
                        viewModel.Musics = SearchMusics();
                        viewModel.Albums = SearchAlbums();
                        viewModel.Artists = SearchArtists();
                        viewModel.Genres = SearchGenres();
                        break;
                    }
            }
            return View("SearchResults", viewModel);
        }
    }
}
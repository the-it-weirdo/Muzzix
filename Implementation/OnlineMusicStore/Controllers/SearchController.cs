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

        public IActionResult SearchByLanguage(string language)
        {
            _logger.LogInformation(language);

            return View("SearchResults", new SearchViewModel
            {
                QueryString = $"Musics in {language}.",
                Musics = _repository.SearchByLanguage(language)
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

        public IActionResult SearchAll(string allType)
        {
            var viewModel = new SearchViewModel();
            viewModel.QueryString = $"All {allType}";
            switch (allType)
            {
                case "Artist":
                    {
                        viewModel.Artists = _repository.SearchArtists();
                        break;
                    }
                case "Album":
                    {
                        viewModel.Albums = _repository.SearchAlbums();
                        break;
                    }
                case "Music":
                    {
                        viewModel.Musics = _repository.SearchMusics();
                        break;
                    }
                case "Genre":
                    {
                        viewModel.Genres = _repository.SearchGenres();
                        break;
                    }
                case "Language":
                    {
                        viewModel.MusicsByLanguage = _repository.SearchByLanguage();
                        break;
                    }
                default:
                    {
                        viewModel.Musics = _repository.SearchMusics();
                        viewModel.Albums = _repository.SearchAlbums();
                        viewModel.Artists = _repository.SearchArtists();
                        viewModel.Genres = _repository.SearchGenres();
                        break;
                    }
            }
            return View("SearchResults", viewModel);
        }
    }
}
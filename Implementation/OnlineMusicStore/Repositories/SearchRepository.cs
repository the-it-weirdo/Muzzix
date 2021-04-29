using OnlineMusicStore.Models;
using OnlineMusicStore.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;



namespace OnlineMusicStore.Repositories
{
    public class SearchRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public SearchRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public List<Album> SearchAlbums(string query = "")
        {
            var albums = _dbContext.Albums.Where(al => al.Name.Contains(query)).ToList();
            return albums;
        }

        public List<Music> SearchMusics(string query = "")
        {
            var musics = _dbContext.Musics.Where(m => m.Name.Contains(query)).ToList();
            musics.Sort((music1, music2) => { return music1.DateReleased.CompareTo(music2.DateReleased); });
            return musics;
        }

        public List<Artist> SearchArtists(string query = "")
        {
            var artists = _dbContext.Artists
            .Where(a => a.Name.Contains(query))
            .OrderBy(a => a.Name)
            .ToList();
            return artists;
        }

        public List<Genre> SearchGenres(string query = "")
        {
            var genres = _dbContext.Genres
            .Where(al => al.Name.Contains(query))
            .OrderBy(g => g.Name)
            .ToList();
            return genres;
        }

        public List<Music> SearchByLanguage(string language = "")
        {
            var musics = _dbContext.Musics
            .Include(m => m.Artists)
            .Include(m => m.Album)
            .Include(m => m.Genre)
            .Where(m => m.Language.Contains(language))
            .OrderBy(m => m.Name)
            .ToList();

            musics.Sort((music1, music2) => { return music1.DateReleased.CompareTo(music2.DateReleased); });

            return musics;
        }

        public List<Music> SearchByGenre(string genre = "")
        {
            var musics = _dbContext.Musics
            .Include(m => m.Genre)
            .Where(m => m.Genre.Name.Contains(genre))
            .ToList();

            musics.Sort((music1, music2) => { return music1.DateReleased.CompareTo(music2.DateReleased); });

            return musics;
        }

        public List<Music> SearchMusicByArtist(Artist artist)
        {
            if (artist == null)
                return null;

            var musics = _dbContext.Musics
            .Include(m => m.Artists)
            .Where(m => m.Artists.Contains(artist))
            .ToList();

            musics.Sort((music1, music2) => { return music1.DateReleased.CompareTo(music2.DateReleased); });

            return musics;
        }

        public List<Album> SearchAlbumByArtist(Artist artist)
        {
            if (artist == null)
                return null;

            var albums = _dbContext.Albums
            .Include(al => al.Artists)
            .Where(al => al.Artists.Contains(artist))
            .ToList();

            return albums;
        }

        public Artist SearchArtist(int id)
        {
            return _dbContext.Artists.SingleOrDefault(a => a.Id == id);
        }
    }
}
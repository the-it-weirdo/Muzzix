using System;
using System.Collections.Generic;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.ViewModels
{
    public class SearchViewModel
    {
        public string QueryString { get; set; }

        public IEnumerable<Album> Albums { get; set; }

        public IEnumerable<Music> Musics { get; set; }

        public IEnumerable<Music> MusicsByLanguage { get; set; }

        public IEnumerable<Artist> Artists { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}
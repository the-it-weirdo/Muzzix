using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Music> Musics { get; private set; }

        public ICollection<Artist> Artists { get; private set; }

        // public ICollection<Genre> Genres { get; private set; }

        public Album()
        {
            Musics = new HashSet<Music>();
            // Artists = new HashSet<Artist>();
            // Genres = new HashSet<Genre>();
        }

        public void AddMusic(Music music)
        {
            Musics.Add(music);
        }

        public void AddArtist(Artist artist)
        {
            Artists.Add(artist);
        }

        // public void AddGenre(Genre genre)
        // {
        //     Genres.Add(genre);
        // }
    }
}
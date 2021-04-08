using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Rating { get; set; }

        public virtual ICollection<Album> Albums { get; private set; }

        public virtual ICollection<Music> Musics { get; private set; }

        public Artist()
        {
            Albums = new HashSet<Album>();
            Musics = new HashSet<Music>();
        }
    }
}
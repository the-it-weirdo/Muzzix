using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Music> Musics { get; private set; }

        public Genre()
        {
            Musics = new HashSet<Music>();
        }
    }
}
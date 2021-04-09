using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Music
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Language { get; set; }

        [Display(Name = "Release Date")]
        public DateTime DateReleased { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Album")]
        public int? AlbumId { get; set; }

        public Album? Album { get; set; }

        public virtual ICollection<Artist> Artists { get; private set; }

        public Music()
        {
            Artists = new HashSet<Artist>();
        }

        public void AddArtist(Artist artist)
        {
            Artists.Add(artist);
        }
    }
}
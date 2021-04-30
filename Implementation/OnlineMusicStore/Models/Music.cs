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

        [Required]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

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

        public virtual ICollection<Artist> Artists { get; set; }

        public bool IsRecentlyAdded()
        {
            return DateAdded.HasValue ? CheckRecent(DateAdded.Value) : false;
        }

        public bool IsRecentlyReleased()
        {
            return CheckRecent(DateReleased);
        }

        private bool CheckRecent(DateTime initialTime)
        {
            if ((DateTime.Now - initialTime).TotalDays <= 7)
            {
                return true;
            }
            return false;
        }

        // public Music()
        // {
        //     // Artists = new HashSet<Artist>();
        // }

        // // public void AddArtist(Artist artist)
        // // {
        // //     Artists.Add(artist);
        // // }

        // // public void RemoveArtist(Artist artist)
        // // {
        // //     Artists.Remove(artist);
        // // }
    }
}
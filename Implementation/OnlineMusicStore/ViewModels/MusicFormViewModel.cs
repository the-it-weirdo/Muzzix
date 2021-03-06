using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using OnlineMusicStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineMusicStore.ViewModels
{
    public class MusicFormViewModel
    {
        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Music" : "Add New Music";
            }
        }

        public int? Id { get; set; }

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
        [Required]
        public DateTime DateReleased { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public SelectList Genres { get; private set; }

        public void SetGenres(IEnumerable<Genre> Genres)
        {
            this.Genres = new SelectList(Genres, "Id", "Name");
        }

        public MultiSelectList Artists { get; private set; }

        public void SetArtists(IEnumerable<Artist> Artist)
        {
            this.Artists = new MultiSelectList(Artist, "Id", "Name");
        }

        [Display(Name = "Artists")]
        [Required]
        public List<int> SelectedArtistIds { get; set; }


        public MusicFormViewModel()
        {
            Id = 0;
            SelectedArtistIds = new List<int>();
            DateReleased = DateTime.Now;
        }

        public MusicFormViewModel(Music music)
        {
            Id = music.Id;
            Name = music.Name;
            Language = music.Language;
            ImageUrl = music.ImageUrl;
            DateReleased = music.DateReleased;
            GenreId = music.GenreId;
            Price = music.Price;
            SelectedArtistIds = music.Artists.Select(ar => ar.Id).ToList();
        }

        public Music MapToMusicObject()
        {
            var music = new Music
            {
                Id = Id ?? 0, // If right hand Id is null, left hand Id is assigned 0.
                Name = Name,
                Language = Language,
                ImageUrl = ImageUrl,
                DateReleased = DateReleased,
                GenreId = GenreId,
                Price = Price,
                Artists = new HashSet<Artist>() // Populated in Controller
                // AlbumId = AlbumId ?? 0
            };
            return music;
        }
    }
}
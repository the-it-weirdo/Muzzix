using System;
using System.ComponentModel.DataAnnotations;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.ViewModels
{
    public class ArtistFormViewModel
    {
        public string Title
        {
            get
            {
                return Id!= 0? "Edit Artist" : "Add New Artist";
            }
        }

        public int? Id {get;set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public float Rating {get; set;}

        public ArtistFormViewModel()
        {
            Id = 0;
        }

        public ArtistFormViewModel(Artist artist)
        {
            Id = artist.Id;
            Name = artist.Name;
            Rating = artist.Rating;
        }
    }
}
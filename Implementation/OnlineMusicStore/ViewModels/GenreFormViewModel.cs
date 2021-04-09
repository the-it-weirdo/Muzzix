using System;
using System.ComponentModel.DataAnnotations;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.ViewModels
{
    public class GenreFormViewModel
    {
        public string Title
        {
            get
            {
                return Id!= 0? "Edit Genre" : "Add New Genre";
            }
        }

        public int? Id {get;set;}

        [Required]
        public string Name {get; set;}

        public GenreFormViewModel()
        {
            Id = 0;
        }

        public GenreFormViewModel(Genre Genre)
        {
            Id = Genre.Id;
            Name = Genre.Name;
        }
    }
}
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using OnlineMusicStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineMusicStore.ViewModels
{
    public class AlbumFormViewModel
    {
        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Album" : "Add New Album";
            }
        }

        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public SelectList Musics { get; private set; }

        public void SetMusic(IEnumerable<Music> Musics)
        {
            this.Musics = new SelectList(Musics, "Id", "Name");
        }

        [Display(Name = "Musics")]
        [Required]
        public List<int> SelectedMusicIds { get; set; }

        public AlbumFormViewModel()
        {
            Id = 0;
            SelectedMusicIds = new List<int>();
        }

        public AlbumFormViewModel(Album album)
        {
            this.Id = album.Id;
            this.Name = album.Name;
            this.ImageUrl = album.ImageUrl;
            this.SelectedMusicIds = album.Musics.Select(m => m.Id).ToList();
        }
    }
}
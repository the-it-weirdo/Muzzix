using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineMusicStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineMusicStore.ViewModels
{
    public class RequestViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Requesting for")]
        [Required]
        public REQUEST_TYPE RequestType { get; set; }

        [Display(Name = "Request Message")]
        [Required]
        public string RequestMessage { get; set; }

        public SelectList ListOfRequestTypes { get; private set; }

        public RequestViewModel()
        {
            ListOfRequestTypes = new SelectList(new List<REQUEST_TYPE>()
            {
                REQUEST_TYPE.Music,
                REQUEST_TYPE.Album,
                REQUEST_TYPE.Artist,
                REQUEST_TYPE.Genre
            });
        }
    }
}
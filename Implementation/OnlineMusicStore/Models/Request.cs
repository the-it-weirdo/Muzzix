using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{

    public enum REQUEST_TYPE
    {
        Music = 1, 

        Album = 2, 

        Artist = 3, 

        Genre = 4
    }

    public class Request : IContantEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public REQUEST_TYPE RequestType { get; set; }

        [Required]
        public string RequestMessage { get; set; }
    }
}
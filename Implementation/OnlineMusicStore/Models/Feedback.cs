using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Feedback : IContantEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public float Rating { get; set; }

        [Required]
        public string FeedbackMessage { get; set; }
    }
}
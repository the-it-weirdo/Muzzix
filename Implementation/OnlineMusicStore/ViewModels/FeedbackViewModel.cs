using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.ViewModels
{
    public class FeedbackViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public float Rating { get; set; }

        [Display(Name = "Feedback Message")]
        [Required]
        public string FeedbackMessage { get; set; }
    }
}
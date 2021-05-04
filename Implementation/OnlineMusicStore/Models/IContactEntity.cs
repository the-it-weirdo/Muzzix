using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public interface IContantEntity
    {
        int Id { get; set; }

        [Required]
        string Name { get; set; }

        [Required]
        string Email { get; set; }
    }
}
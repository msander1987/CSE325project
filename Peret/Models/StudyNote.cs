using System.ComponentModel.DataAnnotations;

namespace Peret.Models
{
    public class StudyNote
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string BookSetId { get; set; } = "bom-en";

        public int CampaignNumber { get; set; } = 1;

        [Required]
        [MaxLength(100)]
        public string Book { get; set; } = string.Empty;

        public int Chapter { get; set; }

        public int? Verse { get; set; }

        [Required(ErrorMessage = "Please write your note before saving.")]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
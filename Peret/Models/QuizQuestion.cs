using System.ComponentModel.DataAnnotations;

namespace Peret.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }

        [Required]
        public string CreatedByUserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string BookSetId { get; set; } = "bom-en";

        public int CampaignNumber { get; set; } = 1;

        [Required]
        [MaxLength(100)]
        public string Book { get; set; } = string.Empty;

        public int Chapter { get; set; }

        [Required(ErrorMessage = "Question text is required.")]
        [MaxLength(500)]
        public string QuestionText { get; set; } = string.Empty;

        [Required(ErrorMessage = "Correct answer is required.")]
        [MaxLength(200)]
        public string CorrectAnswer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Option B is required.")]
        [MaxLength(200)]
        public string OptionB { get; set; } = string.Empty;

        [Required(ErrorMessage = "Option C is required.")]
        [MaxLength(200)]
        public string OptionC { get; set; } = string.Empty;

        [Required(ErrorMessage = "Option D is required.")]
        [MaxLength(200)]
        public string OptionD { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
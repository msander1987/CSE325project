using System.ComponentModel.DataAnnotations;

namespace Peret.Models
{
    public class UserCampaign
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string BookSetId { get; set; } = "bom-en";

        public int CurrentCampaign { get; set; } = 1;

        [MaxLength(100)]
        public string LastBook { get; set; } = "1 Nephi";

        public int LastChapter { get; set; } = 1;
    }
}
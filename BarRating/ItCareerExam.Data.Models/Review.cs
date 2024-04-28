using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [Required]
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Bar))]
        public int BarId { get; set; }

        [Required]
        public Bar Bar { get; set; } = null!;

        public int Score { get; set; }

        [Required]
        [MaxLength(ReviewTextMaxLength)]
        public string Text { get; set; } = null!;
    }
}

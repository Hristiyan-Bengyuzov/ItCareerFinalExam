using ItCareerExam.Data.Models;
using ItCareerExam.Services.Mapping;
using System.ComponentModel.DataAnnotations;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Web.DTOs.Reviews
{
    public class CreateReviewDTO : IMapTo<Review>
    {
        public string UserId { get; set; } 
        public int BarId { get; set; }

        [Required]
        [MaxLength(ReviewTextMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        [Range(ReviewScoreMin, ReviewScoreMax)]
        public int Score { get; set; }
    }
}

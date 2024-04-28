using System.ComponentModel.DataAnnotations;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Data.Models
{
    public class Bar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BarNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(BarDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public string? Photo { get; set; }

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}

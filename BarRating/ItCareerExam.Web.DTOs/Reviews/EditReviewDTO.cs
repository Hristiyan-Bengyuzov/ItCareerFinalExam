using AutoMapper;
using ItCareerExam.Data.Models;
using ItCareerExam.Services.Mapping;
using System.ComponentModel.DataAnnotations;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Web.DTOs.Reviews
{
    public class EditReviewDTO : IMapFrom<Review>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ReviewTextMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        [Range(ReviewScoreMin, ReviewScoreMax)]
        public int Score { get; set; }

        public string? Bar { get; set; }
        public string? Photo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Review, EditReviewDTO>()
                .ForMember(dest => dest.Bar, opt => opt.MapFrom(src => src.Bar.Name))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Bar.Photo));
        }
    }
}

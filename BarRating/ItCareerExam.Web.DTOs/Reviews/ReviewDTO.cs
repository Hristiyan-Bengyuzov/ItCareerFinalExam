using AutoMapper;
using ItCareerExam.Data.Models;
using ItCareerExam.Services.Mapping;

namespace ItCareerExam.Web.DTOs.Reviews
{
    public class ReviewDTO : IMapFrom<Review>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Bar { get; set; } = null!;
        public string? Photo { get; set; }
        public string Text { get; set; } = null!;
        public int Score { get; set; }
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.Bar, opt => opt.MapFrom(src => src.Bar.Name))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Bar.Photo))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}

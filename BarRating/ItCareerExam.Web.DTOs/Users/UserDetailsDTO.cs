using AutoMapper;
using ItCareerExam.Data.Models;
using ItCareerExam.Services.Mapping;

namespace ItCareerExam.Web.DTOs.Users
{
    public class UserDetailsDTO : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserDetailsDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}

using AutoMapper;
using ItCareerExam.Data.Models;
using ItCareerExam.Services.Mapping;

namespace ItCareerExam.Web.DTOs.Users
{
    public class UserDTO : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
        }
    }
}

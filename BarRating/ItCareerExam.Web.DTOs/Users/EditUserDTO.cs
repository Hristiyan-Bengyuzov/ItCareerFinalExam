using AutoMapper;
using ItCareerExam.Data.Models;
using ItCareerExam.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace ItCareerExam.Web.DTOs.Users
{
    public class EditUserDTO : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, EditUserDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
        }
    }
}

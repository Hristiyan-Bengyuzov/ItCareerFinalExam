using AutoMapper;
using ItCareerExam.Data.Models;
using ItCareerExam.Infrastructure.Attributes;
using ItCareerExam.Infrastructure.Extensions;
using ItCareerExam.Services.Mapping;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Web.DTOs.Bars
{
    public class CreateBarDTO : IMapTo<Bar>, IHaveCustomMappings
    {
        [Required]
        [MaxLength(BarNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(BarDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [PhotoUpload(BarPhotoMaxSize, new string[] { ".jpg", ".png", ".jfif", ".webp", ".jpeg" })]
        public IFormFile Photo { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CreateBarDTO, Bar>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo.ToBase64String()));
        }
    }
}

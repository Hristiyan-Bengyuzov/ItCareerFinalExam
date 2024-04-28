using ItCareerExam.Data.Models;
using ItCareerExam.Infrastructure.Attributes;
using ItCareerExam.Services.Mapping;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Web.DTOs.Bars
{
    public class EditBarDTO : IMapFrom<Bar>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BarNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(BarDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public string? Photo { get; set; }

        [Required]
        [PhotoUpload(BarPhotoMaxSize, new string[] { ".jpg", ".png", ".jpeg", ".jfif" })]
        public IFormFile PhotoInput { get; set; } = null!;
    }
}

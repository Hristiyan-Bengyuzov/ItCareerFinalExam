using ItCareerExam.Data.Models;
using ItCareerExam.Services.Mapping;

namespace ItCareerExam.Web.DTOs.Bars
{
    public class BarDTO : IMapFrom<Bar>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Photo { get; set; }
    }
}

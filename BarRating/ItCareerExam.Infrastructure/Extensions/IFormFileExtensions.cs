using Microsoft.AspNetCore.Http;

namespace ItCareerExam.Infrastructure.Extensions
{
    public static class IFormFileExtensions
    {
        public static string ToBase64String(this IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var bytes = stream.ToArray();
                return $"data:{file.ContentType};base64,{Convert.ToBase64String(bytes)}";
            }
        }
    }
}

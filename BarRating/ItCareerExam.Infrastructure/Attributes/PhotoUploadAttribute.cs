using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ItCareerExam.Infrastructure.Attributes
{
    public class PhotoUploadAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;
        private readonly string[] _allowedExtensions;

        public PhotoUploadAttribute(long maxFileSize, string[] allowedExtensions)
        {
            _maxFileSize = maxFileSize;
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return new ValidationResult("The attribute must be used on a file");
            }

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult("File size exceeds the maximum allowed size.");
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!Array.Exists(_allowedExtensions, ext => ext.Equals(extension)))
            {
                return new ValidationResult("Invalid file type. Please upload a photo file.");
            }

            return ValidationResult.Success;
        }
    }
}

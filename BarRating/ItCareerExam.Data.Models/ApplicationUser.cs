using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ItCareerExam.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string FirstName { get; set; } = null!;

		[Required]
		public string LastName { get; set; } = null!;

        public IEnumerable<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}

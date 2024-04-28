using ItCareerExam.Data.Models;
using static ItCareerExam.Common.GlobalConstants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ItCareerExam.Data.Seeders
{
	public class UserSeeder : ISeeder
	{
		public async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			if (userManager.Users.Any()) return;

			var adminToCreate = new ApplicationUser
			{
				UserName = "admin",
				Email = "admin@admin.com",
				FirstName = "Admin",
				LastName = "Adminov",
			};

			var userToCreate = new ApplicationUser
			{
				UserName = "Hristiyan",
				Email = "hristiyan@abv.bg",
				FirstName = "Hristiyan",
				LastName = "Bengyuzov"
			};

			var admin = await userManager.CreateAsync(adminToCreate, "Admin123");
			var user = await userManager.CreateAsync(userToCreate, "Parola!123");

			if (admin.Succeeded) await userManager.AddToRoleAsync(adminToCreate, AdministratorRole);
			if (user.Succeeded) await userManager.AddToRoleAsync(userToCreate, UserRole);
		}
	}
}

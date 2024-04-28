using static ItCareerExam.Common.GlobalConstants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ItCareerExam.Data.Seeders
{
	public class RoleSeeder : ISeeder
	{
		public async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			if (await roleManager.FindByNameAsync(AdministratorRole) is null)
			{
				await roleManager.CreateAsync(new IdentityRole(AdministratorRole));
			}

			if (await roleManager.FindByNameAsync(UserRole) is null)
			{
				await roleManager.CreateAsync(new IdentityRole(UserRole));
			}
		}
	}
}

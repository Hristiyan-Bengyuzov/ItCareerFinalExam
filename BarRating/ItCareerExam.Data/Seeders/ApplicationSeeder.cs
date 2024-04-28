namespace ItCareerExam.Data.Seeders
{
	public class ApplicationSeeder : ISeeder
	{
		public async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
		{
			if (context is null) throw new ArgumentNullException(nameof(context));

			if (serviceProvider is null) throw new ArgumentNullException(nameof(serviceProvider));

			var seeders = new List<ISeeder>
			{
				new RoleSeeder(),
				new UserSeeder(),
			};

			foreach (var seeder in seeders)
			{
				await seeder.SeedAsync(context, serviceProvider);
			}
		}
	}
}

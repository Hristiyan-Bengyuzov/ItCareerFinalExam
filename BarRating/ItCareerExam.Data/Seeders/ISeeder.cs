namespace ItCareerExam.Data.Seeders
{
	public interface ISeeder
	{
		Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider);
	}
}

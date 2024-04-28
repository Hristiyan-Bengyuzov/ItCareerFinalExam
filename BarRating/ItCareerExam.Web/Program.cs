using ItCareerExam.Data;
using ItCareerExam.Data.Models;
using ItCareerExam.Data.Repositories;
using ItCareerExam.Data.Seeders;
using ItCareerExam.Services.Data.Bars;
using ItCareerExam.Services.Data.Reviews;
using ItCareerExam.Services.Data.Users;
using ItCareerExam.Services.Mapping;
using ItCareerExam.Web.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<ApplicationDbContext>();

// protection against CSRF
builder.Services.AddControllersWithViews();
// register repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// register data services
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IBarsService, BarsService>();
builder.Services.AddTransient<IReviewsService, ReviewsService>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    new ApplicationSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
}

// create automapper for the DTOs assembly
AutoMapperConfig.RegisterMappings(typeof(CreateUserDTO).Assembly);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

await app.RunAsync();

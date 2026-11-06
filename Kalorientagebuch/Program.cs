using Kalorientagebuch.Data;
using Kalorientagebuch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
     ?? throw new InvalidOperationException("Connection string 'KalorientagebuchDbContext' not found.");

builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddDbContext<KalorientagebuchDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*builder.Services.AddDbContext<KalorientagebuchDbContext>(options =>
    options.UseInMemoryDatabase(connectionString));*/

builder.Services.AddAuthentication("loginCookieAuth").AddCookie("loginCookieAuth", options =>
{
    options.Cookie.Name = "loginCookieAuth";
    options.LoginPath = "/User/Login";
    options.AccessDeniedPath = "/User/AccessDenied";
});



var app = builder.Build();

// Configure the HTTP request pipeline.     
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<KalorientagebuchDbContext>();

if (dbContext.Users.Count() == 0) {
 dbContext.AddRange(
    new User
    {
        //Id = 1,
        Name = "Max Mustermann",
        Birthday = new DateOnly(1990, 1, 1),
        Height = 180,
        Weight = 75,
        Gender = "male",
        PasswordHash = new PasswordHasher<User>().HashPassword(new User(), "password123"),
        Picture = new MemoryStream().ToArray()
    },
    new User
    {
        //Id = 2,
        Name = "Erika Musterfrau",
        Birthday = new DateOnly(1992, 5, 15),
        Height = 165,
        Weight = 60,
        Gender = "female",
        PasswordHash = new PasswordHasher<User>().HashPassword(new User(), "password456"),
        Picture = new MemoryStream().ToArray()
    }
);

dbContext.SaveChanges();
}

app.Run();

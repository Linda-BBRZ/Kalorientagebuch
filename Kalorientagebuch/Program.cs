using Kalorientagebuch.Data;
using Kalorientagebuch.Interfaces;
using Kalorientagebuch.Mapper;
using Kalorientagebuch.Models;
using Kalorientagebuch.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
     ?? throw new InvalidOperationException("Connection string 'KalorientagebuchDbContext' not found.");

builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

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
    pattern: "{controller=Entry}/{action=Index}/{id?}")
    .WithStaticAssets();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<KalorientagebuchDbContext>();

if(dbContext.ActivityLevels.Count() ==0)
{
    dbContext.AddRange(
        new ActivityLevel
        {
            Factor = 1.2,
            Caption = "überwiegend sitzende Tätigkeit (wenig oder keine Bewegung)"
        },
        new ActivityLevel
        {
            Factor = 1.375,
            Caption = "leicht aktiv (leichte Bewegung/Sport 1-3 Tage/Woche)"
        },
        new ActivityLevel
        {
            Factor = 1.55,
            Caption = "mäßig aktiv (moderate Bewegung/Sport an 3-5 Tagen/Woche)"
        },
        new ActivityLevel
        {
            Factor = 1.725,
            Caption = "sehr aktiv (anstrengende Bewegung/Sport an 6-7 Tagen pro Woche)"
        },
        new ActivityLevel
        {
            Factor = 1.9,
            Caption = "besonders aktiv bist (sehr viel Bewegung/Sport und eine körperliche Arbeit)"
        }
    );
    dbContext.SaveChanges();
}

if (dbContext.Users.Count() == 0)
{
    dbContext.AddRange(
       new User
       {
           //Id = 1,
           Name = "Max Mustermann",
           Birthday = new DateOnly(1990, 1, 1),
           Height = 180,
           Weight = 75,
           Gender = "male",
           ActivityLevel = dbContext.ActivityLevels.FirstOrDefault(al => al.Factor == 1.375),
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
           ActivityLevel = dbContext.ActivityLevels.FirstOrDefault(al => al.Factor == 1.2),
           PasswordHash = new PasswordHasher<User>().HashPassword(new User(), "password456"),
           Picture = new MemoryStream().ToArray()
       }


   );

    dbContext.SaveChanges();
}

if (dbContext.CategoryTyps.Count() == 0)
{
    dbContext.AddRange(
        new CategoryTyp
        {
            //Id = 1,
            Caption = "Kalorienaufnahme"
        },
        new CategoryTyp
        {
            //Id = 2,
            Caption = "Kalorienverbrauch"
        }
    );
    dbContext.SaveChanges();
}

if (dbContext.Categories.Count() == 0)
{
    dbContext.AddRange(new Category
    {
        Caption = "Essen",
        CategoryTypeId = dbContext.CategoryTyps.FirstOrDefault(ct => ct.Caption == "Kalorienaufnahme").Id,
        Color = "FF0000" // Beispiel-Farbe für Essen"
    }, new Category
    {
        Caption = "Trinken",
        CategoryTypeId = dbContext.CategoryTyps.FirstOrDefault(ct => ct.Caption == "Kalorienaufnahme").Id,
        Color = "0000FF" // Beispiel-Farbe für Trinken"
    }, new Category
    {
        Caption = "Sport",
        CategoryTypeId = dbContext.CategoryTyps.FirstOrDefault(ct => ct.Caption == "Kalorienverbrauch").Id,
        Color = "00FF00" // Beispiel-Farbe für Sport"
    }, new Category
    {
        Caption = "Grundumsatz",
        CategoryTypeId = dbContext.CategoryTyps.FirstOrDefault(ct => ct.Caption == "Kalorienverbrauch").Id,
        Color = "FFFF00" // Beispiel-Farbe für Grundumsatz"
    });
    dbContext.SaveChanges();
}

app.Run();

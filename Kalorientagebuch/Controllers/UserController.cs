using Kalorientagebuch.Data;
using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kalorientagebuch.Controllers
{
    public class UserController : Controller
    {
        private readonly KalorientagebuchDbContext _context;

        private readonly IPasswordHasher<User> _passwordHasher;
        public UserController(KalorientagebuchDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserViewmodel userViewmodel)
        {
            if(!ModelState.IsValid)   
            {
                return View(userViewmodel);
            }

            var user = new User
            {
                Birthday = userViewmodel.Birthday,
                Height = userViewmodel.Height,
                Name = userViewmodel.Name,
                //PasswordHash = userViewmodel.Password,
                Weight = userViewmodel.Weight
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, userViewmodel.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}

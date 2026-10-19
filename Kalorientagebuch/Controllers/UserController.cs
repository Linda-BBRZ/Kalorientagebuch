using Kalorientagebuch.Data;
using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace Kalorientagebuch.Controllers
{
    public class UserController : Controller
    {
        private readonly KalorientagebuchDbContext _context;
        public UserController(KalorientagebuchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
           //var allUsers = _context.Users.ToList();

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
                PasswordHash = userViewmodel.Password,
                Weight = userViewmodel.Weight
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}

using Kalorientagebuch.Data;
using Kalorientagebuch.Models;
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
           

            return View(new User());
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            
            _context.Users.Add(user);
            _context.SaveChanges();

            return View();
        }
    }
}

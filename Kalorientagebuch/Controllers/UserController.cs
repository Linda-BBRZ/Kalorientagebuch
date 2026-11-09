using Kalorientagebuch.Data;
using Kalorientagebuch.Mapper;
using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

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
            List<UserViewmodel> users = _context.Users.Select(u => UserMapper.Map(u)).ToList();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            UserViewmodel userViewmodel = UserMapper.Map(user);
            calculateTotalDailyEnergyExpenditure(userViewmodel);
            return View(userViewmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            SetViewData();
            if (id == null)
            {
                return NotFound();
            }

            //find user by id
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserViewmodel userViewmodel = UserMapper.Map(user);
            return View(userViewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewmodel userViewmodel)
        {
            SetViewData();
            if (userViewmodel.Id <= 0)
            {
                return NotFound();
            }
            /*if(!ModelState.IsValid)
            {
                return View(userViewmodel);
            }*/
            var user = await _context.Users.FindAsync(userViewmodel.Id);
            if (user == null)
            {
                return NotFound();
            }

            UserMapper.Map(user, userViewmodel, _passwordHasher);
            if (userViewmodel.PictureFile != null && userViewmodel.PictureFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                userViewmodel.PictureFile.CopyTo(memoryStream);
                user.Picture = memoryStream.ToArray();
            }
            _context.Update(user);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public IActionResult Index(UserViewmodel userViewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewmodel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewmodel userViewmodel)
        {
            SetViewData();
            if (!ModelState.IsValid)
            {
                return View(userViewmodel);
            }

            var user = UserMapper.Map(userViewmodel);
            user.PasswordHash = _passwordHasher.HashPassword(user, userViewmodel.Password);

            if (userViewmodel.PictureFile != null && userViewmodel.PictureFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                userViewmodel.PictureFile.CopyTo(memoryStream);
                user.Picture = memoryStream.ToArray();
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Image(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.Picture == null)
                return NotFound();

            return File(user.Picture.ToArray(), "image/png");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == loginViewModel.Name);
            //var user = await _context.Users.FindAsync(loginViewModel.Name);
            var user = _context.Users.FirstOrDefault(u => u.Name == loginViewModel.Name);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User nicht gefunden.");
                return View(loginViewModel);
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginViewModel.Password);

            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError(string.Empty, "Passwort ist falsch.");
                return View(loginViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "loginCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("loginCookieAuth", principal);

            return RedirectToAction("Details", "User", UserMapper.Map(user));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("loginCookieAuth");
            return RedirectToAction("Login", "User");
        }

        public IActionResult AccessDenied()
        {
            return Content("Zugriff verweigert.");
        }

        public double CalculateBasalMetabolicRate(UserViewmodel userViewmodel)
        {
            double basalMetabolicRate = 0;
            if (userViewmodel.Gender == "male")
            {
                // Für Männer: BMR = 66,5 + (13,75 × Gewicht in kg) + (5,003 × Größe in cm) - (6,75 × Alter)
                basalMetabolicRate = 66.5 + (13.75 * userViewmodel.Weight) + (5.003 * userViewmodel.Height)
                    - (6.755 * calculateAge(userViewmodel.Birthday));
            }
            else if (userViewmodel.Gender == "female")
            {
                // Für Frauen: BMR = 655,1 + (9, 563 × Gewicht in kg) +(1, 850 × Körpergröße in cm) -(4, 676 × Alter)
                basalMetabolicRate = 655.1 + (9.563 * userViewmodel.Weight) + (1.850 * userViewmodel.Height)
                    - (4.676 * calculateAge(userViewmodel.Birthday));
            }
            else
            {
                // Geschlecht unbekannt oder nicht angegeben, Standardwert verwenden
                basalMetabolicRate = 0;
            }
            return basalMetabolicRate;
        }

        //TDEE (Total Daily Energy Expenditure)
        public void calculateTotalDailyEnergyExpenditure(UserViewmodel userViewmodel)
        {
            double calculateBasalMetabolicRate = CalculateBasalMetabolicRate(userViewmodel);
            double factor = _context.ActivityLevels.FirstOrDefault(a => a.Id == userViewmodel.ActivityLevelId).Factor;
            if(calculateBasalMetabolicRate > 0 && factor > 0)
            {
                //userViewmodel.calculatedTDEE = Math.Round(calculateBasalMetabolicRate * factor);
                userViewmodel.calculatedTDEE = Convert.ToInt32(calculateBasalMetabolicRate * factor);
            }
        }

        private int calculateAge(DateOnly birthday)
        {
            int age = DateTime.Today.Year - birthday.Year;
            // TODO: Check if the birthday has already occurred this year, and adjust the age accordingly
            return age;
        }

        private void SetViewData()
        {
            var activityLevels = _context.ActivityLevels.ToList();
            ViewData["ActivityLevels"] = activityLevels;
        }
    }
}

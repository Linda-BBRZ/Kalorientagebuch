using Kalorientagebuch.Data;
using Kalorientagebuch.Interfaces;
using Kalorientagebuch.Mapper;
using Kalorientagebuch.Models;
using Kalorientagebuch.Models.Requests;
using Kalorientagebuch.Models.Responses;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Kalorientagebuch.Controllers
{
    public class UserController : Controller
    {
        private readonly KalorientagebuchDbContext _context;

        private readonly IPasswordHasher<Models.User> _passwordHasher;

        private readonly IUserService _userService;

        private readonly IUserMapper _userMapper;
        public UserController(KalorientagebuchDbContext context, IPasswordHasher<User> passwordHasher, 
            IUserService userService, IUserMapper userMapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _userService = userService;
            _userMapper = userMapper;

        }

        [HttpGet]
        public IActionResult Index()
        {
            IndexUserViewModel indexUserViewModel = new IndexUserViewModel();
            indexUserViewModel.Items = _context.Users.Select(u => _userMapper.Map(u)).ToList();

            return View(indexUserViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            //var user = await _context.Users.Include(u => u.ActivityLevel).FindAsync(id);
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            UserDetailResponse userDetailResponse = _userMapper.MapUserDetailResponse(user);
            userDetailResponse.calculatedTDEE = _userService.CalculateTotalDailyEnergyExpenditure(user);
            
            return View(userDetailResponse);
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

            UserEditRequest userEditRequest = _userMapper.MapUserEditRequest(user);
            return View(userEditRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditRequest userEditRequest)
        {
            SetViewData();
            if (userEditRequest.Id <= 0)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return View(userEditRequest);
            }
            var user = await _context.Users.FindAsync(userEditRequest.Id);
            if (user == null)
            {
                return NotFound();
            }

            _userMapper.Map(user, userEditRequest);
            if (userEditRequest.PictureFile != null && userEditRequest.PictureFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                userEditRequest.PictureFile.CopyTo(memoryStream);
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
        public IActionResult Create(UserCreateRequest userCreateRequest)
        {
            SetViewData();
            if (!ModelState.IsValid)
            {
                return View(userCreateRequest);
            }

            var user = _userMapper.Map(userCreateRequest);
            user.PasswordHash = _passwordHasher.HashPassword(user, userCreateRequest.Password);

            if (userCreateRequest.PictureFile != null && userCreateRequest.PictureFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                userCreateRequest.PictureFile.CopyTo(memoryStream);
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

            return base.RedirectToAction("Details", "User", _userMapper.Map(user));
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

        private void SetViewData()
        {
            var activityLevels = _context.ActivityLevels.ToList();
            ViewData["ActivityLevels"] = activityLevels;
        }
    }
}

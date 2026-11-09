using Kalorientagebuch.Data;
using Kalorientagebuch.Mapper;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Kalorientagebuch.Controllers
{
    public class EntryController : Controller
    {
        private readonly KalorientagebuchDbContext _context;

        public EntryController(KalorientagebuchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EntryViewModel> entries = _context.Entries.Select(e => EntryMapper.Map(e)).ToList();
            return View(entries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EntryViewModel entryViewModel)
        {
            SetViewData();
            if(ModelState.IsValid)
            {
                entryViewModel.UserId = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Id;
                _context.Entries.Add(EntryMapper.Map(entryViewModel));
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Entry");
        }

        private void SetViewData()
        {
            var categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;
        }
    }
}

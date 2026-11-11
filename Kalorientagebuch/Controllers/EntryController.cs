using Kalorientagebuch.Data;
using Kalorientagebuch.Mapper;
using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            List<EntryViewModel> entries = await _context.Entries.Include(c => c.Category)
                .Select(e => EntryMapper.Map(e)).ToListAsync();
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SetViewData();
            var entry = _context.Entries.FirstOrDefault(e => e.Id == id);
            if (entry == null)
            {
                return NotFound();
            }
            EntryViewModel entryViewModel = EntryMapper.Map(entry);
            return View(entryViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EntryViewModel entryViewModel)
        {
            SetViewData();
            if (ModelState.IsValid)
            {
                var entry = _context.Entries.FirstOrDefault(e => e.Id == entryViewModel.Id);
                if (entry == null)
                {
                    return NotFound();
                }
                //entryViewModel.UserId = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Id;
                EntryMapper.Map(entry, entryViewModel);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Entry");
        }

        public IActionResult Delete(int id)
        {
            var entry = _context.Entries.FirstOrDefault(e => e.Id == id);
            if (entry == null)
            {
                return NotFound();
            }
            _context.Entries.Remove(entry);
            _context.SaveChanges();
            return RedirectToAction("Index", "Entry");
        }

        private void SetViewData()
        {
            var categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;
        }
    }
}

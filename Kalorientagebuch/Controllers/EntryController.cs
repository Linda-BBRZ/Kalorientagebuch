using Kalorientagebuch.Data;
using Kalorientagebuch.Interfaces;
using Kalorientagebuch.Mapper;
using Kalorientagebuch.Models.Requests;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kalorientagebuch.Controllers
{
    public class EntryController : Controller
    {
        private readonly KalorientagebuchDbContext _context;

        private readonly IEntryMapper _entryMapper;

        public EntryController(KalorientagebuchDbContext context, IEntryMapper entryMapper)
        {
            _context = context;
            _entryMapper = entryMapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexEntryViewModel indexEntryViewModel = new IndexEntryViewModel();
            indexEntryViewModel.Entries = await _context.Entries.Include(c => c.Category)
                .Select(e => _entryMapper.Map(e)).ToListAsync();
            return View(indexEntryViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EntryCreateRequest entryCreateRequest)
        {
            SetViewData();
            if (ModelState.IsValid)
            {
                entryCreateRequest.UserId = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Id;
                _context.Entries.Add(_entryMapper.Map(entryCreateRequest));
                _context.SaveChanges();
                return RedirectToAction("Index", "Entry");
            }
            else
            {
                return View(entryCreateRequest);
            }


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
            EntryEditRequest entryEditRequest = _entryMapper.MapEntryEditRequest(entry);
            return View(entryEditRequest);
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
                _entryMapper.Map(entry, entryViewModel);
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

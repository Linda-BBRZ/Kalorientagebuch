using Kalorientagebuch.Data;
using Kalorientagebuch.Interfaces;
using Kalorientagebuch.Models.Requests;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kalorientagebuch.Controllers
{
    public class EntryController : Controller
    {
        private readonly KalorientagebuchDbContext _context;

        private readonly IEntryMapper _entryMapper;
        private readonly IEntryService _entryService;

        public EntryController(KalorientagebuchDbContext context, IEntryMapper entryMapper, IEntryService entryService)
        {
            _context = context;
            _entryMapper = entryMapper;
            _entryService = entryService;
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
            EntryCreateRequest entryCreateRequest = new EntryCreateRequest();
            entryCreateRequest.Categories = new SelectList(_context.Categories.ToList(), "Id", "Caption");
            return View(entryCreateRequest);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Save(EntryCreateRequest entryCreateRequest)
        { 
            //SetViewData();
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ModelState.AddModelError(string.Empty, "Sie müssen angemeldet sein, um einen Eintrag zu erstellen.");
            //}
            entryCreateRequest.Categories = new SelectList(_context.Categories.ToList(), "Id", "Caption");

            //if (!entryCreateRequest.CategoryId.HasValue)
            //{
            //    ModelState.AddModelError(nameof(entryCreateRequest.CategoryId), "CategoryId darf nicht leer sein");
            //    return View("~/Views/Entry/Create.cshtml", entryCreateRequest);
            //}
            if (!entryCreateRequest.CaloriesAmount.HasValue)
            {
                ModelState.AddModelError(nameof(entryCreateRequest.CaloriesAmount), "Kalorienmenge darf nicht leer sein");
                return View("~/Views/Entry/Create.cshtml", entryCreateRequest);
            }

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
        public IActionResult Edit(EntryEditRequest entryEditRequest)
        {
            SetViewData();
            if (ModelState.IsValid)
            {
                var entry = _context.Entries.FirstOrDefault(e => e.Id == entryEditRequest.Id);
                if (entry == null)
                {
                    return NotFound();
                }
                //entryViewModel.UserId = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Id;
                _entryMapper.Map(entry, entryEditRequest);
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

        public IActionResult Details(int id)
        {
            var entry = _context.Entries.Include(c => c.Category).FirstOrDefault(e => e.Id == id);
            if (entry == null)
            {
                return NotFound();
            }
            var entryDetailResponse = _entryMapper.MapEntryDetailResponse(entry);
            return View(entryDetailResponse);
        }

        [HttpPost]
        public IActionResult Create(EntryCreateRequest request)
        {
            EntryCreateRequest entryCreateRequest = _entryService.GetProductInfosByBarcode(request.Barcode);

            int? selectedCategory = null;

            // Match the selected category from the request with the categories in the database
            if (entryCreateRequest.Categories != null && entryCreateRequest.Categories.Any())
            {
                foreach (var category in entryCreateRequest.Categories)
                {
                    if(category.Value.Contains("beverages"))
                    {
                        selectedCategory = _context.Categories.FirstOrDefault(c => c.Caption == "Trinken").Id;
                        break;
                    }
                }

                if(selectedCategory == null)
                {
                    selectedCategory = _context.Categories.FirstOrDefault(c => c.Caption == "Essen").Id;
                }

                //if (entryCreateRequest.Categories.Items.)
                //{
                //    selectedCategory = _context.Categories.FirstOrDefault(c => c.Caption == "Trinken").Id;
                //} else
                //{
                //    selectedCategory = _context.Categories.FirstOrDefault(c => c.Caption == "Essen").Id;
                //}
            }
            entryCreateRequest.CategoryId = selectedCategory;
            entryCreateRequest.Categories = new SelectList(_context.Categories.ToList(), "Id", "Caption", selectedCategory);

            //return View("~/Views/Entry/Create.cshtml", entryCreateRequest);
            ModelState.Clear();
            return View(entryCreateRequest);
        }

        private void SetViewData()
        {
            var categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;
        }
    }
}

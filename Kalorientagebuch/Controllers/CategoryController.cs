using Kalorientagebuch.Data;
using Kalorientagebuch.Mapper;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kalorientagebuch.Controllers
{
    public class CategoryController : Controller
    {
        private readonly KalorientagebuchDbContext _context;

        public CategoryController(KalorientagebuchDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IndexCategoryViewModel indexCategoryViewModel = new IndexCategoryViewModel();
            indexCategoryViewModel.Categories = _context.Categories.Include(c => c.CategoryType)
                .Select(c => new IndexItemCategoryViewModel
                {
                    Id = c.Id,
                    Caption = c.Caption,
                    Color = "#" + c.Color,
                    CategoryTypeCaption = c.CategoryType.Caption
                }).ToList();

            return View(indexCategoryViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(CategoryCreateRequest request)
        {
            SetViewData();
            if (ModelState.IsValid)
            {
                _context.Categories.Add(CategoryMapper.Map(request));
                _context.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View(request);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SetViewData();
            var category = _context.Categories.Include(c => c.CategoryType).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryViewModel categoryViewModel = CategoryMapper.Map(category);
            return View(categoryViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel categoryViewModel)
        {
            SetViewData();
            if (ModelState.IsValid)
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == categoryViewModel.Id);
                if (category == null)
                {
                    return NotFound();
                }
                CategoryMapper.Map(categoryViewModel, category);
                _context.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View(categoryViewModel);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            SetViewData();
            var category = _context.Categories.Include(c => c.CategoryType).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryViewModel categoryViewModel = CategoryMapper.Map(category);
            return View(categoryViewModel);
        }

        private void SetViewData()
        {
            ViewData["CategoryTypes"] = _context.CategoryTyps.Select(ct => CategoryTypMapper.Map(ct)).ToList();
        }
    }
}

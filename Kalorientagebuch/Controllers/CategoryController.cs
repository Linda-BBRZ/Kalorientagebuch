using Kalorientagebuch.Data;
using Kalorientagebuch.Mapper;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Mvc;

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
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            foreach(var cat in _context.Categories)
            {
                categories.Add(CategoryMapper.Map(cat));
            }
            return View(categories);
        }
    }
}

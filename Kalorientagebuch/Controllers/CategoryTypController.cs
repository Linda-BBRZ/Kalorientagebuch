using Kalorientagebuch.Data;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace Kalorientagebuch.Controllers
{
    public class CategoryTypController : Controller
    {
        private readonly KalorientagebuchDbContext _context;

        public CategoryTypController(KalorientagebuchDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CategoryTypViewModel> categoryTyps = _context.CategoryTyps.Select(ct => new CategoryTypViewModel
            {
                Id = ct.Id,
                Caption = ct.Caption
            }).ToList();

            return View(categoryTyps);
        }
    }
}

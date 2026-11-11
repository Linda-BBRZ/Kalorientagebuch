using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Mapper
{
    public class CategoryMapper
    {
        public static Category Map(CategoryCreateRequest request)
        {
            Category category = new Category();
            category.Caption = request.Caption;
            category.CategoryTypeId = request.CategoryTypeId;
            category.Color = request.Color?.TrimStart('#');
            return category;
        }

        public static CategoryViewModel Map(Category category)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Caption = category.Caption,
                CategoryTypeId = category.CategoryTypeId,
                Color = "#" + category.Color
            };
            return categoryViewModel;
        }

        public static void Map(CategoryViewModel categoryViewModel, Category category)
        {
            category.Id = categoryViewModel.Id;
            category.Caption = categoryViewModel.Caption;
            category.CategoryTypeId = categoryViewModel.CategoryTypeId;
        }

    }
}

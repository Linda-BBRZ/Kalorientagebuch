using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Mapper
{
    public class CategoryMapper
    {
        public static Category Map(CategoryViewModel categoryViewModel)
        {
            Category category = new Category();
            category.Id = categoryViewModel.Id;
            category.Caption = categoryViewModel.Caption;
            category.CategoryTypeId = categoryViewModel.CategoryType.Id;
            return category;
        }

        public static CategoryViewModel Map(Category category)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Caption = category.Caption,
                CategoryType = getMappedCategoryTyp(category.CategoryTypeId)
            };
            return categoryViewModel;
        }

        public static void Map(CategoryViewModel categoryViewModel, Category category)
        {
            category.Id = categoryViewModel.Id;
            category.Caption = categoryViewModel.Caption;
            category.CategoryTypeId = categoryViewModel.CategoryType.Id;
        }

        private static CategoryTypViewModel getMappedCategoryTyp(int categoryTypId)
        {
            CategoryTypViewModel categoryTypViewModel = new CategoryTypViewModel();
            categoryTypViewModel.Id = categoryTypId;
            categoryTypViewModel.Caption = "WORKAROUND";

            return categoryTypViewModel;
        }


    }
}

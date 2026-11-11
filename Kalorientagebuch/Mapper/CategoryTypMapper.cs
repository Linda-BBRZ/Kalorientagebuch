using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Mapper
{
    public class CategoryTypMapper
    {
        public static CategoryTypViewModel Map(CategoryTyp? categoryType)
        {
            CategoryTypViewModel categoryTypViewModel = new CategoryTypViewModel
            {
                Id = categoryType?.Id ?? 0,
                Caption = categoryType?.Caption
            };
            return categoryTypViewModel;
        }

        public static CategoryTyp Map(CategoryTypViewModel categoryTypViewModel)
        {
            CategoryTyp categoryTyp = new CategoryTyp();
            categoryTyp.Id = categoryTypViewModel.Id;
            categoryTyp.Caption = categoryTypViewModel.Caption;
           
            //foreach (var categoryViewModel in categoryTypViewModel.Categories)
            //{
            //    categoryTyp.Categories.Add(CategoryMapper.Map(categoryViewModel));
            //}

            //Categories = categoryTypViewModel.Categories?.Select(c => new Category
            //{
            //    Id = c.Id,
            //    Caption = c.Caption,
            //    CategoryTypeId = categoryTypViewModel.Id
            //}).ToList()

            return categoryTyp;
        }
    }
}

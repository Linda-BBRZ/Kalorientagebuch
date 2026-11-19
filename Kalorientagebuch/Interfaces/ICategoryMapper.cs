using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Interfaces
{
    public interface ICategoryMapper
    {
        public Category Map(CategoryCreateRequest request);

        public CategoryViewModel Map(Category category);

        public void Map(CategoryViewModel categoryViewModel, Category category);


    }
}

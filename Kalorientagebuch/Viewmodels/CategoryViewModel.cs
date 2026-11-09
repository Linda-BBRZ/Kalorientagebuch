using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Viewmodels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [MinLength(3)]
        public string? Caption { get; set; }

        public CategoryTypViewModel? CategoryType { get; set; }
    }
}

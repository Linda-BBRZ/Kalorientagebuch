using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Viewmodels
{
    public class CategoryTypViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Beschreibung:")]
        [MaxLength(50)]
        [MinLength(3)]
        public string Caption { get; set; }

        public List<CategoryViewModel>? Categories { get; set; } = new List<CategoryViewModel>();

    }
}

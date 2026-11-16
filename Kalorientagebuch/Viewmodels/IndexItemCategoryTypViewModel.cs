using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Viewmodels
{
    public class IndexItemCategoryTypViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Beschreibung:")]
        [MaxLength(50)]
        [MinLength(3)]
        public string Caption { get; set; }
    }
}

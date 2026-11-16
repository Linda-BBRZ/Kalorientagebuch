using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalorientagebuch.Viewmodels
{
    [NotMapped]
    public record CategoryCreateRequest
    {
        [MaxLength(50)]
        [MinLength(3)]
        public string? Caption { get; set; }

        [Display(Name = "Farbe")]
        [MinLength(7)]
        [MaxLength(7)]
        public string? Color { get; set; }

        public int CategoryTypeId { get; set; }
    }
}

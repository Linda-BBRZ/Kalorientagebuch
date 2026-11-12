using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Models.Responses
{
    public class UserDetailResponse
    {
        public int Id { get; set; }

        [Display(Name = "Name:"), MaxLength(25), MinLength(2)]
        public string Name { get; set; }

        [Display(Name = "Geburtsdatum:")]
        public DateOnly Birthday { get; set; }

        [Display(Name = "Gewicht in kg:"), Range(0, 1000)]
        public double Weight { get; set; }

        [Display(Name = "Größe in cm:"), Range(0, 1000)]
        public int Height { get; set; }

        [Display(Name = "Geschlecht:")]
        public string Gender { get; set; }

        public IFormFile? PictureFile { get; set; }

        [Display(Name = "TDEE (kcal):")]
        public int calculatedTDEE { get; set; }
    }
}

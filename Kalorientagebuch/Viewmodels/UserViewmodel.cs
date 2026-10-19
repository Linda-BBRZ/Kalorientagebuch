using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Viewmodels
{
    public class UserViewmodel
    {
        public int Id { get; set; }

        [Display(Name = "Name:")]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        [Display(Name = "Passwort:")]
        [MaxLength(25)]
        [MinLength(5)]
        public string Password { get; set; }

        [Display(Name = "Geburtsdatum:")]
        public DateOnly Birthday { get; set; }

        [Display(Name = "Gewicht in kg:")]
        [Range(0, 1000)]
        public double Weight { get; set; }

        [Display(Name = "Größe in m:")]
        [Range(0, 25)]
        public double Height { get; set; }


    }
}

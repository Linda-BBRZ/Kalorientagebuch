using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Viewmodels
{
    public class LoginViewModel
    {
        [Display(Name = "Name:")]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        [Display(Name = "Passwort:")]
        [MaxLength(25)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}

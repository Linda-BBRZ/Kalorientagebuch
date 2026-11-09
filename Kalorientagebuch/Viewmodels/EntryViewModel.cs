using Kalorientagebuch.Models;
using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Viewmodels
{
    public class EntryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Beschreibung")]
        public string? Description { get; set; }

        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [Display(Name = "Kalorienmenge")]
        public int CaloriesAmount { get; set; }

        [Display(Name = "Kategorie")]
        public int CategoryId { get; set; }

        [Display(Name = "Nutzer")]
        public int UserId { get; set; }
    }
}

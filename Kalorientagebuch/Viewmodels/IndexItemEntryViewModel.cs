using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Viewmodels
{
    public class IndexItemEntryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Beschreibung"), MaxLength(100)]
        public string? Description { get; set; }

        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [Display(Name = "Kalorienmenge")]
        public int CaloriesAmount { get; set; }

        [Display(Name = "Kategorie")]
        public int CategoryId { get; set; }

        [Display(Name = "Nutzer")]
        public int UserId { get; set; }

        public string Color { get; set; }
    }
}

using Kalorientagebuch.Viewmodels;
using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Models.Requests
{
    public record EntryCreateRequest
    {
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

    }
}

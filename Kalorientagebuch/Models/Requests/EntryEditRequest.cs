using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalorientagebuch.Models.Requests
{
    [NotMapped]
    public record EntryEditRequest
    {
        public int Id { get; set; }

        [Display(Name = "Beschreibung"), MaxLength(100)]
        public string? Description { get; set; }

        [Display(Name = "Datum")]
        public DateOnly Date { get; set; }

        [Display(Name = "Kalorienmenge")]
        public int CaloriesAmount { get; set; }

        [Display(Name = "Kategorie")]
        public int CategoryId { get; set; }

        [Display(Name = "Nutzer")]
        public int UserId { get; set; }
    }
}

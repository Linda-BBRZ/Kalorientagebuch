using System.ComponentModel.DataAnnotations.Schema;

namespace Kalorientagebuch.Models.Responses
{
    [NotMapped]
    public record CategoryDetailResponse
    {
        public int Id { get; set; }

        public string Caption { get; set; }
        
        public string? Color { get; set; }

        public string CategoryTypeCaption { get; set; }
    }
}

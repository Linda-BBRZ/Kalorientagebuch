namespace Kalorientagebuch.Models.Responses
{
    public record CategoryDetailResponse
    {
        public int Id { get; set; }

        public string Caption { get; set; }
        
        public string? Color { get; set; }

        public string CategoryTypeCaption { get; set; }
    }
}

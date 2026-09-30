namespace Kalorientagebuch.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Caption { get; set; }

        public int CategoryTypeId { get; set; }

        public CategoryTyp CategoryType { get; set; }

        public List<Entry> Entries { get; set; } = new List<Entry>();
    }
}

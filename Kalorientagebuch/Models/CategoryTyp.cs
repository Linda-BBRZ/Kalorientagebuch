namespace Kalorientagebuch.Models
{
    public class CategoryTyp
    {
        public int Id { get; set; }

        public string Caption { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}

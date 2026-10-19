using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Models
{
    public class CategoryTyp
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Caption { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}

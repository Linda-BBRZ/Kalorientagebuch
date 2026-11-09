namespace Kalorientagebuch.Models
{
    public class ActivityLevel
    {
        public int Id { get; set; }

        public double Factor { get; set; }

        public string Caption { get; set; }

        public List<User>? Users { get; set; } = new List<User>();
    }
}
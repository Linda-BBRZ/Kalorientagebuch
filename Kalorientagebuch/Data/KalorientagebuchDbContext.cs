using Microsoft.EntityFrameworkCore;

namespace Kalorientagebuch.Data
{
    public class KalorientagebuchDbContext : DbContext
    {
        public KalorientagebuchDbContext(DbContextOptions<KalorientagebuchDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Entry> Entries { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.CategoryTyp> CategoryTyps { get; set; }
        public DbSet<Models.ActivityLevel> ActivityLevels { get; set; }

    }
}

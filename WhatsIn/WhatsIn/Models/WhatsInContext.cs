using Microsoft.EntityFrameworkCore;

namespace WhatsIn.Models
{
    public class WhatsInContext : DbContext
    {
        public WhatsInContext(DbContextOptions options) : base(options) { }

        // each DB Set is a table
        public DbSet<Product> Products { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
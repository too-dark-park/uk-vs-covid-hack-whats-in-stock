using Microsoft.EntityFrameworkCore;

namespace WhatsIn.Models
{
    public class WhatsInContext : DbContext
    {
        public WhatsInContext(DbContextOptions<WhatsInContext> options)
          : base(options)
        { }


        // each DB Set is a table
        public DbSet<Product> Products { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Place> Places { get; set; }
    }
}
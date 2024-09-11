using Microsoft.EntityFrameworkCore;

namespace TwitterAPI.Entities
{
    public class TwitterContext : DbContext
    {
        private IConfiguration _configuration;
        public TwitterContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Entity Setsa
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Likes> Likes { get; set; } 

        // Ensures the Email property is unique
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<User>().HasIndex(u=>u.Email).IsUnique();
        }

        //Configure ConnectionString

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TwitterConnection"));
        }
    }
}

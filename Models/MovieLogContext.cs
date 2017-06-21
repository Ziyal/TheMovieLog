using Microsoft.EntityFrameworkCore;
 
namespace MovieLog.Models
{
    public class MovieLogContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MovieLogContext(DbContextOptions<MovieLogContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Follower> Followers { get; set; }

    }
}
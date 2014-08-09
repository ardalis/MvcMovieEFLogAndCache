using System.Data.Entity;
using System.Diagnostics;

namespace MvcMovie.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.Log = s => Debug.Print(s);
            base.OnModelCreating(modelBuilder);
        }
    }
}
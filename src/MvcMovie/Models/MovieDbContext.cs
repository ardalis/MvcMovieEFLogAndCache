using System.Data.Entity;
using System.Diagnostics;

namespace MvcMovie.Models
{
    public class MovieDbContext : DbContext
    {
      public DbSet<Movie> Movies { get; set; }

        public MovieDbContext()
        {
            Database.Log = s => Debug.Print(s);
        }
    }
}
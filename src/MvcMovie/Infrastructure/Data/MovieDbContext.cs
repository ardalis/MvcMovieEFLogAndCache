using System.Data.Entity;
using System.Diagnostics;
using MvcMovie.Models;

namespace MvcMovie.Infrastructure.Data
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
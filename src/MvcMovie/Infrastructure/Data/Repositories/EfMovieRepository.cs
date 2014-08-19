using System;
using System.Collections.Generic;
using System.Linq;
using MvcMovie.Core.Interfaces;
using MvcMovie.Models;

namespace MvcMovie.Infrastructure.Data.Repositories
{
    public class EfMovieRepository : IMovieRepository
    {
        private readonly MovieDbContext db = new MovieDbContext();
        public IEnumerable<string> ListGenres()
        {
            var genreLst = new List<string>();

            var genreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            genreLst.AddRange(genreQry.Distinct());

            return genreLst.AsEnumerable();
        }

        public IEnumerable<Movie> ListMovies(string movieGenre, string searchString)
        {
            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            return movies.AsEnumerable();
        }
    }
}
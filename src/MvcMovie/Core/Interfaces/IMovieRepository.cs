using System.Collections.Generic;
using MvcMovie.Models;

namespace MvcMovie.Core.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<string> ListGenres();
        IEnumerable<Movie> ListMovies(string movieGenre, string searchString);
    }
}
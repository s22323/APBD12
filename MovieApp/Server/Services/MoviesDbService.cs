using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Data;
using MovieApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Server.Services
{
    public interface IMoviesRepository
    {

    }

    public interface IMoviesDbService
    {
        Task<List<Movie>> GetMovies();
        Task AddMovie(Movie movie);
        Task<Movie> GetMovie(int movieId);
        Task<Movie> UpdateMovie(Movie movie);
        Task<Movie> DeleteMovie(int movieId);
    }

    public class MoviesDbService : IMoviesDbService
    {
        private ApplicationDbContext _context;

        public MoviesDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<Movie> GetMovie(int movieId)
        {
            return _context.Movies.FirstOrDefault(x => x.Id == movieId);
        }

        public Task<List<Movie>> GetMovies()
        {
            return _context.Movies.OrderBy(m => m.Title).ToListAsync();
        }

        Task<Movie> DeleteMovie(int movieId)
        {
           Movie movie = _context.Movies.FirstOrDefault(x ==> x.Id == movieId);
            if (movie !== null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
                return movie;
            }
            return null;
        }

        Task<Movie> UpdateMovie(Movie movie)
        {
            Movie resultMovie = _context.Movies.FirstOrDefault(x ==> x.Id == movie.Id);
            if (resultMovie !== null)
            {
                resultMovie.movieId == movie.MovieId;
                resultMovie.Title == movie.Title;
                resultMovie.Summary = movie.Summary;
                resultMovie.InTheaters = movie.InTheaters;
                resultMovie.Trailer = movie.Trailer;
                resultMovie.ReleaseDate = movie.ReleaseDate;
                resultMovie.Poster = movie.Poster;

                _context.SaveChanges();
                return resultMovie;
            }
            return null;
        }
    }
}

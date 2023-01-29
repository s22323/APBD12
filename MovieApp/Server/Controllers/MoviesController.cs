using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApp.Server.Services;
using System.Threading.Tasks;


namespace MovieApp.Server.Controllers
{
    [Authorize]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesDbService _dbService;

        public MoviesController(ILogger<MoviesController> logger, IMoviesDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _dbService.GetMovies());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Movie>> GetMovie(int idMovie)
        {
            var result = await _dbService.GetMovie(idMovie);
            if (result == null)
            {
                return NotFound("There is not such a movie");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            return (Ok(await _dbService.AddMovie(movie)));
        }

        [HttpPut]
        public async Task<ActionResult<Movie>> UpdateMovie(Movie movie)
        {
            return Ok(await _dbService.UpdateMovie(movie));
        }

        [HttpDelete]
        public async Task<ActionResult<Movie>> DeleteMovie(int idMovie)
        {
            return Ok(await _dbService.DeleteMovie(idMovie));
        }
    }
}

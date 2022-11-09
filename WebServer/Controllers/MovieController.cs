using AutoMapper;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("movies")]
    [ApiController]

    public class MovieController : ControllerBase
    {
        private IDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public MovieController(IDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetSearch(string searchType, string title, string? plot = null, string? character = null, string? name = null)

        {
            if (searchType == "structured")
            {
                var result = _dataService.getStructuredSearch(title, plot, character, name);
                return Ok(result);
            }
            else if (searchType == "simple")
            {
                var result = _dataService.GetSearch(title);
                return Ok(result);
            }

            else if (searchType == "best")
            {
                var result = _dataService.GetBestMatch(title);
                Console.WriteLine($"Længde er resultat: {result.Count}");
                return Ok(result);
            }
            else return NotFound();
        }



        [HttpGet("{title_id}/similar")]
        public IActionResult GetSimilarMovies(string title_id)
        { 
            var titles =
                _dataService.getSimilarMovies(title_id);
            if (titles == null)
            {
                return NotFound();
            }
            return Ok(titles);
        }

        [HttpGet("{title_id}/popular_actor/")]
        public IActionResult GetPopularActorsFromMovie(string title_id)
        {
         
            var actors =
                _dataService.getPopularActorsFromMovie(title_id);
            if (actors == null)
            {
                return NotFound();
            }
            return Ok(actors);
        }

    }
}

using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("movies")]
    [ApiController]

    public class MovieController : ControllerBase
    {
        private const int MaxPageSize = 25;
        private IMovieDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public MovieController(IMovieDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
     
    }

    

        [HttpGet()]
        public IActionResult GetSearch(string searchType, string title, int page = 0, int pageSize = 10, string? plot= null, string? character = null, string? name = null)

        {
            if (searchType == "structured")
            {
                var result = _dataService.getStructuredSearch(pageSize, page, title, plot, character, name);

                return Ok(result);
            }
            else if (searchType == "simple")
            {
            
                var result = _dataService.GetSearch(title/*, pageSize, page*/);
              
                return Ok(result);
            }

            else if (searchType == "best")
            {
                var result = _dataService.GetBestMatch(title);
                Console.WriteLine($"Længde er resultat: {result.Count}");

                return Ok(result);
            }

            else if (searchType == "exact")
            {
              var result = _dataService.GetExcactSearch(title.ToLower());
              Console.WriteLine($"Længde er resultat: {result.Count}");
              return Ok(result);
            }
            else if (searchType == "word")
            {
                var result = _dataService.GetWordToWord(title.ToLower());
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



    }


}

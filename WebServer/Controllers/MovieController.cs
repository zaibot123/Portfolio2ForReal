using AutoMapper;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Nest;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

namespace WebServer.Controllers
{
    [Route("movies")]
    [ApiController]

    public class MovieController : ControllerBase
    {
        private IMovieDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public MovieController(IMovieDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        private string? CreateLink(int page, int pageSize)
        {
            return _generator.GetUriByName( 
                HttpContext,
                nameof(GetSearch), new { page, pageSize });
     
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
            if (titles.Count == 0) 
            {
                return NotFound();
            }
            return Ok(titles);
        }


        [HttpPut()]
        public IActionResult AssignBookmark(string title_id, string username)
        {     
            _dataService.Bookmark(title_id, username);
            return Ok();
        }

        [HttpDelete()]
        public IActionResult DeleteBookmark(string title_id, string username)
        {
            _dataService.Bookmark(title_id, username);
            return Ok();
        }

    }
}

using AutoMapper;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Nest;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elasticsearch.Net;
using System;
using WebServer.Models;
using static System.Net.WebRequestMethods;

namespace WebServer.Controllers
{
    [Route("api/movies")]
    [ApiController]

    public class MovieController : ControllerBase
    {
        private IMovieDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;
        private int maxPageSize=20;

        public MovieController(IMovieDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }


        private string? CreateLink(string endpoint, object TitleId)
        {
            return _generator.GetUriByName(
                HttpContext,
                endpoint, TitleId);
        }


        [HttpGet(Name = nameof(GetSearch))]
        public IActionResult GetSearch(string searchType, string search, string? plot = null,
                  string? character = null, string? name = null, int page=0, int pagesize=10)

        {
            if (searchType == "structured")
            {
                var result = _dataService.getStructuredSearch(search, plot, character, name);
                return Ok(result);
            }
            else if (searchType == "simple")
            {
                var total = _dataService.getSizeSimpleSearch("Troels", search);
                var result = _dataService.GetSearch(search,page, pagesize);
                var paging=SearchPaging<Titles>(page, pagesize, total, result, nameof(GetSearch),searchType,search);

                return Ok(paging);
            }

            else if (searchType == "best")
            {
                var result = _dataService.GetBestMatch(search);
                Console.WriteLine($"Længde er resultat: {result.Count}");

                return Ok(result);
            }

            else if (searchType == "exact")
            {
                var result = _dataService.GetExcactSearch(search.ToLower());
                Console.WriteLine($"Længde er resultat: {result.Count}");
                return Ok(result);
            }
            else if (searchType == "word")
            {
                var result = _dataService.GetWordToWord(search.ToLower());
                return Ok(result);
            }
            else return NotFound();
        }

        [HttpGet("{title_id}",Name=nameof(GetSingleMovie))]
        public IActionResult GetSingleMovie(string title_id)
        {
            var titles =
                _dataService.GetSingleMovieByID(title_id);
            if (titles.Count == 0)
            {
                return NotFound();
            }
            return Ok(titles);
        }



        [HttpGet("{title_id}/similar")]
        public IActionResult GetSimilarMovies(string title_id)
        {
            List<TitlesModel> MovieList = new List<TitlesModel>();
            var titles =
                _dataService.getSimilarMovies(title_id);
            if (titles.Count == 0)
            {
                return NotFound();
            }

            foreach (var movie in titles)
            {
                Console.WriteLine(movie.id.ToString()); 
                var model = new TitlesModel
                {
                    URL = "http://localhost:5001/api/movies/" +movie.id,
                    Poster = movie.poster,
                    TitleName = movie.name,
                    genre = movie.genre,
                    
                };
                MovieList.Add(model);
            }

            return Ok(MovieList);
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

        private string? CreatePageLink(int page, int pageSize, string endpoint, string searchtype, string search)
        {
            return _generator.GetUriByName(
                HttpContext,
                endpoint, new {searchtype, search,page, pageSize });
        }




        private object SearchPaging<T>(int page, int pageSize, int total, IEnumerable<T> items, string endpoint, string searchtype, string search)
        {
            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize;
            var pages = (int)Math.Ceiling((double)total / (double)pageSize);
            var first = CreatePageLink(0, pageSize, endpoint, searchtype,search);
            var prev = page > 0
                ? CreatePageLink(page - 1, pageSize,endpoint,searchtype,search)
                : null;
            var current = CreatePageLink(page, pageSize, endpoint,searchtype,search);
            var next = page < pages - 1
                ? CreatePageLink(page + 1, pageSize,endpoint,searchtype,search)
                : null;
            var result = new
            {
                first,
                prev,
                next,
                current,
                total,
                pages,
                items
            };
            return result;
        }
    }
}

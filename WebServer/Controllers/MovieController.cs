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


        [HttpGet(Name = nameof(GetSearch))]
        public IActionResult GetSearch(string username, string searchType, string title, string? ID = null, string? plot = "",
                  string? characters = "", string? name = "", int page=0, int pagesize=10)

        {
            if (searchType == "structured")
            {
                //var result = _dataService.getStructuredSearch(title, plot, character, name);
                //return Ok(result);

                var total = _dataService.getSizeStructuredSearch(title, plot, characters, name);
                Console.WriteLine(total);
                Console.WriteLine("TOTAL");
                var result = _dataService.getStructuredSearch(title, plot, characters, name, page, pagesize);

                List<TitlesModel> TitleModelList = new List<TitlesModel>();


                if (result == null)
                {
                    return NotFound();
                }

                foreach (var movie in result)
                {
                    var model = new TitlesModel
                    {
                        URL = "http://localhost:5001/api/movies/" + movie.ID,
                        TitleName = movie.Name,
                        Poster=movie.Poster
                
                       
                    };
                    TitleModelList.Add(model);
                }
                Console.WriteLine(title);
                var paging = SearchPaging<TitlesModel>(page, pagesize, (int)total, TitleModelList, nameof(GetSearch), searchType, title, plot, characters, name);


                return Ok(paging);


            }
                
            else if (searchType == "simple")
            {
                var total = _dataService.getSizeSimpleSearch("Troels", title);
                var result = _dataService.GetSearch(username, title,page, pagesize);
                
                List<TitlesModel> TitleModelList = new List<TitlesModel>();
               

                if (result == null)
                {
                    return NotFound();
                }
                    
                foreach (var movie in result)
                {
                    var titleID = movie.TitleId;    
                    var model = new TitlesModel
                    {
                        URL = "http://localhost:5001/api/movies/"+titleID,
                        ID=movie.TitleId,
                        TitleName = movie.TitleName,
                        Poster = movie.Poster,
                        Plot=movie.TitlePlot,
                        
                    };
                    TitleModelList.Add(model);
                }
                Console.WriteLine(title);
                var paging = SearchPaging<TitlesModel>(page, pagesize, total, TitleModelList, nameof(GetSearch), searchType, title);
    

                return Ok(paging);
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
                var titleID = movie.ID;
                Console.WriteLine(titleID);
                var model = new TitlesModel
                {
                    URL = "http://localhost:5001/api/movies/" + titleID,
                    Poster = movie.Poster,
                    TitleName = movie.Name,
                };
                MovieList.Add(model);
            }

            return Ok(MovieList);
        }


        [HttpPut("bookmark")]
        public IActionResult AssignBookmark(string title_id, string username)
        {
            try
            {
                _dataService.Bookmark(title_id, username);
                return Ok($"201 created bookmark for {username}");
            }
            catch (Npgsql.PostgresException)
            {
                return BadRequest("Unable to add bookmark. have you already bookmarked?");
            }
        }

        [HttpDelete("bookmark")]
        public IActionResult DeleteBookmark(string title_id, string username)
        {
            _dataService.DeleteBookmark(title_id, username);
            return Ok($"204 deleted bookmark for {username}");
        }


        [HttpGet("genre/{title_id}")]
        public IActionResult GetGenresForTitle(string title_id)
        {

            var genreModel = new HasGenreModel();
            genreModel.Genre =
                _dataService.getGenresForTitle(title_id)
                .Select(x => x.Genre).ToList();
   
            if (genreModel.Genre.Count == 0)
            {
                return NotFound("This title has no genre");
            }

            return Ok(genreModel);
        }


        [HttpGet("popular")]
        public IActionResult getPopularMovies()
        {

            List<RatingHistoryModel> MovieRatingList = new List<RatingHistoryModel>();

            var result =
               _dataService.getPopularMovies();


            if (result.Count == 0)
            {
                return NotFound();
            }

            foreach (var item in result)
            {
                var movie = new RatingHistoryModel
                {
                    Rating = item.AvgRating,
                    TitleName = item.Name,
                    URL = "http://localhost:5001/api/movies/" + item.ID,
                    titleId = item.ID,
                    Poster = item.Poster
                   
                };
                MovieRatingList.Add(movie);
            }
            return Ok(MovieRatingList.Where(c => c.Rating != null));
        }


        private string? CreatePageLink(int page, int pageSize, string endpoint, string searchtype, string title, string plot, string characters, string name)
        {
            if (searchtype == "simple")
            {

                return _generator.GetUriByName(
                    HttpContext,
                    endpoint, new { searchtype, title, page, pageSize });
            }

            else if (searchtype == "structured")
            {
                return _generator.GetUriByName(
                HttpContext,
                endpoint, new { searchtype, title, plot, characters, name, page, pageSize});
            }
            else return null;
        }

        private object SearchPaging<T>(int page, int pageSize, int total, IEnumerable<T> items, string endpoint, 
            string searchtype, string title, string plot="", string characters= "", string name="")
        {
            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize;
            var pages = (int)Math.Ceiling((double)total / (double)pageSize);
            var first = CreatePageLink(0, pageSize, endpoint, searchtype,title, plot, characters, name);
            var prev = page > 0
                ? CreatePageLink(page - 1, pageSize,endpoint,searchtype,title, plot, characters, name)
                : null;
            var current = CreatePageLink(page, pageSize, endpoint,searchtype,title, plot, characters, name);
            var next = page < pages - 1
                ? CreatePageLink(page + 1, pageSize,endpoint,searchtype,title, plot, characters, name)
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

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

        [HttpGet("simple/{user_input}")]
        public IActionResult getTitles(string user_input)
        {
            var titles =
                _dataService.getSearch(user_input);
            if (titles == null)
            {
                return NotFound();
            }
            return Ok(titles);
        }

        [HttpGet("structured/title:{title}&plot:{plot}&characters:{characters}&actorname:{actorname}")]
        public IActionResult GetStructuredSearch(string title, string plot, string characters, string actorname)
        
        {
            Console.WriteLine("JAAA");
                var titles =
                   _dataService.getStructuredSearch(title, plot, characters, actorname);
                if (titles == null)
                {
                    return NotFound();
                }
                return Ok(titles);
            }
        

        [HttpGet("similar/{title_id}")]
        public IActionResult GetSimilarMovies(string title_id)
        {
            Console.WriteLine("HEEELLOOOOO");
            var titles =
                _dataService.getSimilarMovies(title_id);
            if (titles == null)
            {
                return NotFound();
            }
            return Ok(titles);
        }

        [HttpGet("popular_actor/{title_id}")]
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


        /*
            [Route("api/products")]
            [ApiController]
            public class ProductController : ControllerBase
            {
                private IDataService _dataService;
                private readonly LinkGenerator _generator;
                private readonly IMapper _mapper;
                public ProductController(IDataService dataService, LinkGenerator generator, IMapper mapper)
                {
                    _dataService = dataService;
                    _generator = generator;
                    _mapper = mapper;
                }
            [HttpGet("{id}")]
            public IActionResult GetProduct(int id)
            {
                var product =
                    _dataService.GetProduct(id);
                if (product == null)
                {
                    return NotFound();  
                }
                return Ok(product);
            }
            [HttpGet("category/{categoryId}")]
            public IActionResult GetListOFProduct(int categoryId)
            {
                var product =
                    _dataService.GetProductByCategory(categoryId);
                if (!product.Any())
                {
                    return NotFound(product);
                }
                return Ok(product);
            }
            [HttpGet("name/{name}")]
            public IActionResult NameContained(string name)
            {
                var product =
                    _dataService.GetProductByName(name);
                if (!product.Any())
                {
                    return NotFound(product);
                }
                return Ok(product);
            }
        }
            */
    }
}

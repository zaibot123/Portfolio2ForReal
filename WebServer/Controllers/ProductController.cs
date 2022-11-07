using AutoMapper;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("movies")]
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

        [HttpGet("{name}")]
        public IActionResult getTitles(string name)
        {
            var titles =
                _dataService.getTitles(name);
            if (titles == null)
            {
                return NotFound();
            }
            return Ok(titles);
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


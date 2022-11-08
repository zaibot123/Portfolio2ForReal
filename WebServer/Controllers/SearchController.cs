using AutoMapper;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;


namespace WebServer.Controllers
{
    [Route("searchhistory")]
    [ApiController]

    public class SearchController : ControllerBase
    {
        private IDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public SearchController(IDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }
        /*
        [HttpGet()]
        public IActionResult getTitles()
        {
            var search =
                _dataService.getSearch();
            if (search == null)
            {
                return NotFound();
            }
            return Ok(search);
        }
        */
    }
}

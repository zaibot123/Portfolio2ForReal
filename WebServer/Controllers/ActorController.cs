using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;



namespace WebServer.Controllers
{


    [Route("actors")]
    [ApiController]

    public class ActorController : ControllerBase
    {
        private IActorDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public ActorController(IActorDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }


        [HttpGet("{name}/coactors")]
        public IActionResult getCoactors(string name)
        {
            var result=_dataService.getCoActors(name);
            Console.WriteLine(result[0].ActorName);
            return Ok(result);
    
        }

        [HttpGet("{name}/words")]
        public IActionResult getPersonWords(string name)
        {
            var result = _dataService.GetPersonWords(name);
            return Ok(result);
        }

        [HttpGet("popular/{title_id}")]
        public IActionResult GetPopularActorsFromMovie(string title_id)
        {

            var actors =
                _dataService.getPopularActorsFromMovie(title_id);
            if (actors.Count==0)
            {
                return NotFound();
            }
            return Ok(actors);
        }

    }
}
using AutoMapper;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

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

        private string? CreateLink(string endpoint, object? values)
        {

            return _generator.GetUriByName(
                HttpContext,
                endpoint, values);

        }


        [HttpGet("{ID}",Name =nameof(getSingleProffesionalFromId))]
        public IActionResult getSingleProffesionalFromId(string ID)
        {
            var result = _dataService.GetSingleProfessionalFromID(ID);
            if (result == null)
            {
                return NotFound();
            }
            var model = new ProfessionalsModel
            {
                URL = CreateLink(nameof(getSingleProffesionalFromId), new { ID }),
                BirthYear = result.BirthYear,
                DeathYear = result.DeathYear,
                Name = result.ProfName,
                Rating = result.ProfRating
            };
            return Ok(model);
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
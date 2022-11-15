using AutoMapper;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging.Console;
using Nest;
using System.Runtime.InteropServices;
using System.Threading.Channels;
using WebServer.Models;
using static System.Net.WebRequestMethods;

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
            Console.WriteLine(ID.GetType());
            var result = _dataService.GetSingleProfessionalFromID(ID);
            if (result == null)
            {
                return NotFound();
            }
            var model = new ProfessionalsModel
            {
                URL = CreateLink(nameof(getSingleProffesionalFromId), new { ID }),
                //BirthYear = result.BirthYear,
                //DeathYear = result.DeathYear,
                Name = result.ProfName,
               // Rating = result.ProfRating
            };
            return Ok(model);
        }


        [HttpGet("{name}/coactors",Name =nameof(getCoactors))]
        public IActionResult getCoactors(string name)
        {
            List<ProfessionalsModel> ProfList = new List<ProfessionalsModel>();
            var result=_dataService.getCoActors(name);
            if (result == null)
            {
                return NotFound();
            }
            foreach (var actor in result)
            {
                Console.WriteLine(actor.ProfId);
               var model = new ProfessionalsModel
                {
                   //Birth og Death-year skal selectes i Postgres
                    URL = "http://localhost:5001/actors/"+actor.ProfId,
                    Name = actor.ProfName,
                    //BirthYear = actor.BirthYear,
                    //DeathYear = actor.DeathYear,
                    //Rating =actor.ProfRating
                };
                Console.WriteLine(model.URL);
                ProfList.Add(model);
            }
            return Ok(ProfList);
    

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
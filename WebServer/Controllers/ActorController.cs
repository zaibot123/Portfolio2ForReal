using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nest;
using WebServer.Models;


namespace WebServer.Controllers
{
    [Route("api/actors")]
    [ApiController]

    public class ActorController : ControllerBase
    {
        private readonly IMovieDataService _movieDataService;
        private IActorDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;
        private const int MaxPageSize = 20;
   
        public ActorController(IMovieDataService movieDataService, IActorDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _movieDataService = movieDataService;
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
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
                BirthYear = result.BirthYear,
                DeathYear = result.DeathYear,
                Name = result.ProfName,
         
                ID = result.ProfId,
                ProfRating = result.ProfRating

            };
            return Ok(model);
        }


        [HttpGet("coactors/{name}", Name = nameof(getCoactors))]
        public IActionResult getCoactors(string name, int page = 0, int pagesize = 5)
        {
            List<SimpleProfessionalsModel> ProfList = new List<SimpleProfessionalsModel>();
            var result = _dataService.getCoActors(name);

            if (result == null)
            {
                return NotFound();
            }
            foreach (var actor in result)
            {

                var model = new SimpleProfessionalsModel
                {
                    Name = actor.Name,
                    ProfId = actor.ProfId
                };

                ProfList.Add(model);
            }

            return Ok(ProfList);
        }

        [HttpGet("words/{name}")]
        public IActionResult getPersonWords(string name)
        {
            List<WordModel> WordList = new List<WordModel>();
            var result = _dataService.GetPersonWords(name);
            if (result == null)
            {
                return NotFound();
            }
            foreach (var word in result)
            {
                var model = new WordModel
                {
                    value= word.Frequency,
                    text=word.KeyWord
                };
                WordList.Add(model);
            }
            return Ok(WordList);
        }


        [HttpGet("popular/{title_id}")]
        public IActionResult GetPopularActorsFromMovie(string title_id, int page=0, int pagesize=10)
        {

            List<ProfessionalsModel> ProfList = new List<ProfessionalsModel>();
            var result = _dataService.getPopularActorsFromMovie(title_id,page,pagesize);
  
            if (result==null)
            {
             
                return NotFound();
            }
            foreach (var professionals in result)
            {
                var ID = professionals.ProfId;
                var model = new ProfessionalsModel
                {
                    URL = CreateLink(nameof(getSingleProffesionalFromId), new { ID }),
                    Name = professionals.ProfName,
                    //Rating = professionals.ProfRating,
                    BirthYear = professionals.BirthYear,
                    DeathYear = professionals.DeathYear
                };
                ProfList.Add(model);
                }
            return Ok(ProfList);
            
        }


        [HttpGet("search/{user_input}")]
        public IActionResult getSearchOfActors(string user_input)
        {

            List<ProfessionalsModel> ProfList = new List<ProfessionalsModel>();
            var result = _dataService.GetPersonSearch(user_input);

            if (result == null)
            {
                
                return NotFound("hej");
            }
            foreach (var professionals in result)
            {
               
                var model = new ProfessionalsModel
                {
                    Name = professionals.ProfName,
                    BirthYear = professionals.BirthYear,
                    DeathYear = professionals.DeathYear,
                    ID=professionals.ProfId,
                    ProfRating = professionals.ProfRating
                    
                };
                ProfList.Add(model);
            }
            return Ok(ProfList);

        }

        private string? CreateLink(string endpoint, object? values)
        {
            return _generator.GetUriByName(
                HttpContext,
                endpoint, values);
        }



        [HttpGet("characters/{prof_id}")]
        public IActionResult GetCharacters(string prof_id)
        {

            List<String> CharList = new List<String>();
            var characters = _dataService.getCharacters(prof_id);

            foreach (var character in characters)

            {
                CharList.Add(character.Character);
            }

            return Ok(CharList);
        }


        [HttpGet("profession/{prof_id}")]
        public IActionResult GetProfessions(string prof_id)
        {
            List<String> ProfList = new List<String>();
            var result = _dataService.getProfessions(prof_id);

            foreach (var professions in result)

            {
                ProfList.Add(professions.Professions);
            }

            return Ok(ProfList);
        }


        [HttpGet("knownfor/{prof_id}")]
        public IActionResult GetBestKnownFor(string prof_id)
        {
            List<String> MovieList = new List<String>();
            var result = _dataService.getBestKnownFor(prof_id);

            foreach (var movie in result)

            {
                MovieList.Add(movie.TitleNames);
            }

            return Ok(MovieList);
        }






    }
}

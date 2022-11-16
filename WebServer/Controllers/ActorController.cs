using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging.Console;
using Nest;
using System;
using System.Runtime.InteropServices;
using System.Threading.Channels;
using System.Xml.Linq;
using WebServer.Models;
using static System.Net.WebRequestMethods;

namespace WebServer.Controllers
{


    [Route("actors")]
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
                BirthYear = result.BirthYear,
                DeathYear = result.DeathYear,
                Name = result.ProfName,
                //Rating = result.ProfRating
            };
           
            return Ok(model);
        }

        


        [HttpGet("{name}/coactors",Name =nameof(getCoactors))]
        public IActionResult getCoactors(string name,int page=0, int pagesize=5)
        {
            List<ProfessionalsModel> ProfList = new List<ProfessionalsModel>();
            var result=_dataService.getCoActors(name);
            Console.WriteLine(_movieDataService.getSizeSimpleSearch("Troels", "dog"));
            if (result == null)
            {
                return NotFound();
            }
            foreach (var actor in result)
            {
                Console.WriteLine(actor.ProfId);
                var ID = actor.ProfId;
               var model = new ProfessionalsModel
                {
                   
                    URL = CreateLink(nameof(getSingleProffesionalFromId), new { ID }),
                    Name = actor.ProfName,
                    BirthYear = actor.BirthYear,
                    DeathYear = actor.DeathYear,
                   // Rating =actor.ProfRating
                };
                Console.WriteLine(model.URL);
                ProfList.Add(model);
            }

            return Ok(ProfList);
    

        }

        [HttpGet("{name}/words")]
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
                    Frequency= word.Frequency,
                    KeyWord=word.KeyWord

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



        private string? CreatePageLink(int page, int pageSize, string name,string endpoint)
        {

            return _generator.GetUriByName(
                HttpContext,
                nameof(endpoint), new { page, pageSize,name});
        }

    }
}

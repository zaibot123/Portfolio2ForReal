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
        private IActorDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;
        private const int MaxPageSize = 20;


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
                BirthYear = result.BirthYear,
                DeathYear = result.DeathYear,
                Name = result.ProfName,
                //Rating = result.ProfRating
            };
           
            return Ok(model);
        }

        


        [HttpGet("{name}/coactors",Name =nameof(getCoactors))]
        public IActionResult getCoactors(string name,int page, int pagesize)
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
            var paging = Paging(page,pagesize,10,ProfList, "Mads Mikkelsen");


            return Ok(paging);
    

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

        //private string? CreatePageLink(int page, int pageSize, string value)
        //{
        //   // var x = "Mads Mikkelsen";
        //    return _generator.GetUriByName(
        //        HttpContext,
        //        nameof(getCoactors), new {page, pageSize, value});
        //}


        private string? CreatePageLink(int page, int pageSize)
        {
            
            return _generator.GetUriByName(
                HttpContext,
                nameof(getCoactors), new { page, pageSize });
        }

        private object Paging<T>(int page, int pageSize, int total, IEnumerable<T> items, string value)
        {
            pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

            //if (pageSize > MaxPageSize)
            //{
            //    pageSize = MaxPageSize;
            //}

            var pages = (int)Math.Ceiling((double)total / (double)pageSize)
                ;

            var first = CreatePageLink(0, pageSize);
            //var first = total > 0
            //    ? CreatePageLink(0, pageSize)
            //    : null;

            var prev = page > 0
                ? CreatePageLink(page - 1, pageSize)
                : null;

            var current = CreatePageLink(page, pageSize);

            var next = page < pages - 1
                ? CreatePageLink(page + 1, pageSize)
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

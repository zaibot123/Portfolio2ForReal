﻿using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging.Console;
using Nest;
using System;
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
                var ID = actor.ProfId;
               var model = new ProfessionalsModel
                {
                   
                   //Birth og Death-year skal selectes i Postgres
                    URL = CreateLink(nameof(getSingleProffesionalFromId), new { ID }),
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
            if (result.Count==0)
            {
                return NotFound();
            }

            foreach (var professional in ProfList)
            {
                var model = new ProfessionalsModel
                {
                    URL = CreateLink(nameof(getSingleProffesionalFromId), new { title_id }),
                    Name = professional.Name,
                };
                ProfList.Add(model);

            }
            return Ok(ProfList);
        }

    }
}
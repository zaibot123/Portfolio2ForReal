using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;


namespace WebServer.Controllers
{
    [Route("api/register")]
    [ApiController]

    public class registerController : ControllerBase
    {
        private ILoginDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public registerController(ILoginDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult RegisterUser(string username, string password)
        {
            var registered = _dataService.RegisterUser(username, password);
            if (registered) return Ok();
            else return NotFound();


        }

    }
}

using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;


namespace WebServer.Controllers
{
    [Route("api/register")]
    [ApiController]

    public class RegisterController : ControllerBase
    {
        private IuserDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public RegisterController(IuserDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpPost()]
        public IActionResult RegisterUser(string username, string password)
        {
            var registered = _dataService.RegisterUser(username, password);
            if (registered) return Ok($"User {username} registered succesfully");
            else return NotFound($"unable to register user: {username}. Try again with a strong password.");


        }

    }
}

using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/register")]
    [ApiController]

    public class RegisterController : ControllerBase
    {
        private IUserDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public RegisterController(IUserDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpPost()]
        public IActionResult RegisterUser(RegisterModel registerModel)
        {

            var registered = _dataService.RegisterUser(registerModel.Username, registerModel.Password);

            if (registered) return Ok($"User registered succesfully");
            else return NotFound($"unable to register user. Try again.");

        }
    }
}

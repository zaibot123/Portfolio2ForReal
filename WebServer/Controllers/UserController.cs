using AutoMapper;
using DataLayer;
using DataLayer.DataServices;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Secuurity;
using System.Security.Cryptography.X509Certificates;

namespace WebServer.Controllers
{
    [Route("login")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private ILoginDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public UserController(ILoginDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult LoginUser(string username, string hashed_password)
        {
            var data = _dataService.Login(username, hashed_password);
            Authenticator auth = new Authenticator();

            if (username == data[0].UserName && hashed_password == data[0].HashedPassword) {
                Console.WriteLine("SUCCES");
                return Ok("Login Succesful");

            }
            else
            {
                Console.WriteLine("FAIL");
                return BadRequest("Login failed");
            }
           

        }

    }
}

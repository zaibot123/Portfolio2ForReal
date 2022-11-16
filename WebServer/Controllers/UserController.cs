using AutoMapper;
using DataLayer.DataServices;
using DataLayer.Interfaces;
using DataLayer.Model;
using DataLayer.Security;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace WebServer.Controllers
{
    [Route("/api/user")]
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

        [HttpGet("login")]
        public IActionResult LoginUser(string username, string hashed_password)
        {
            var data = _dataService.Login(username, hashed_password);
            Console.WriteLine(data[0].UserName.ToString());
            if (username == data[0].UserName && hashed_password == data[0].HashedPassword) {
                Console.WriteLine("SUCCES");
                return Ok(data);
            }
            else
            {
                Console.WriteLine("FAIL");
                return BadRequest(data);
            }
        }


        [HttpPatch()]
        public IActionResult EditUser(string username, string bio, string photo, string email)
        {
            _dataService.EditUser(username, bio, photo, email);
            return Ok();

        }


    }
}

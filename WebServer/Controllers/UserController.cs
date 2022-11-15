using AutoMapper;
using DataLayer;
using DataLayer.DataServices;
using DataLayer.Model;
using DataLayer.Security;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace WebServer.Controllers
{
    [Route("user")]
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
        [HttpPatch("edit")]
        public IActionResult EditUser(string username, string bio, string photo, string email)
        {
            if (!String.IsNullOrEmpty(username) ){ 
            var data=_dataService.EditUser(username, bio, photo, email);
            return Ok(data);
            }
            return BadRequest();
        }


        [HttpGet("rating")]
        public IActionResult RateMovie(string username, string title_id, int rating)
        {
           _dataService.RateMovie(username, title_id, rating);
            return Ok();
        }

    }
}

using AutoMapper;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;


namespace WebServer.Controllers
{
    [Route("login")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private ILoginDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public LoginController(ILoginDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet()]
        public void RegisterUser(string username, string password)
        {
            _dataService.RegisterUser(username,password);

        }

    }
}

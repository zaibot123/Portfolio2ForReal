using AutoMapper;
using DataLayer.DataServices;
using DataLayer.Interfaces;
using DataLayer.Model;
using DataLayer.Security;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Security.Cryptography.X509Certificates;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("/api/user")]
    [ApiController]

    public class UserController : ControllerBase

    {
        private IuserDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public UserController(IuserDataService dataService, LinkGenerator generator, IMapper mapper)

        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet("{username}")]
        public IActionResult GetSingleUser(string username)
        {

            var user = _dataService.GetSingleUser(username);
            if (user.Count == 0)
            {
                return NotFound($"user {username} not found");
            }
            var model = new User
            {
                Bio = user[0].UserName,
                Email = user[0].Email,
                Photo = user[0].Photo,
                UserName = user[0].UserName,

            };
            return Ok(model);
        }

        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            List<UserModel> UserList = new List<UserModel>();
            var users = _dataService.GetAllUsers();
            foreach (var user in users)
            {
                var model = new UserModel
                {
                    UserName = user.UserName,
                    URL = "http://localhost:5001/api/user/" + user.UserName,
                    Bio = user.Bio,
                    Email = user.Email,
                    Photo = user.Photo
                };
                UserList.Add(model);
            }
            return Ok(UserList);
        }

        [HttpGet("login")]
        public IActionResult LoginUser(string username, string hashed_password)
        {
            var data = _dataService.Login(username, hashed_password);
            if (data.Count == 0)
            {
                return BadRequest();
            }
            else if (username == data[0].UserName && hashed_password == data[0].HashedPassword) {
                return Ok(data);
            }
            else 
            {
                return BadRequest("Fail");
            }
        }

        [HttpPatch("edit")]
        public IActionResult EditUser(string username, string hashed_password, string bio, string photo, string email)  
        {
            _dataService.EditUser(username, bio, photo, email);
            return Ok($"Succesfully updated all information for {username}");
        }


        [HttpPost("rate")]
        public IActionResult PostRating(string username, string title_id, int rating, string hashed_password)
        {
            try
            {
                _dataService.RateMovie(username, title_id, rating);
                return Ok($"Succesfully posted rating of {rating} for {title_id} on behalf of {username}");
            }
            catch (Npgsql.PostgresException e)
            {
                return BadRequest(e.MessageText);
            }

        }

        [HttpDelete("rate")]
        public IActionResult DeleteRating(string username, string title_id, string hashed_password)
        {

             
                _dataService.DeleteMovieRating(username, title_id);
                return Ok($"Succesfully deleted rating of {title_id} on behalf of {username}");

        }

        [HttpGet("{username}/ratings")]
        public IActionResult GetRatingsForPerson(string username)
        {
            var Ratings_for = $"{username}";
            List<RatingHistoryModel> RatingList = new List<RatingHistoryModel>();
            var data = _dataService.GetRatingHistory(username);
            if (data.Count == 0)
            {
                return NotFound("No history found for " + username);
            }

            foreach (var item in data)
            {
                var model = new RatingHistoryModel
                {

                    Rating = item.Rating,
                    TitleName = item.TitleName
                };
                RatingList.Add(model);
                Console.WriteLine(item);
            }
            return Ok(new { Ratings_for, RatingList });
        }
    }
}

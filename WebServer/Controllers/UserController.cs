using AutoMapper;
using DataLayer.DataServices;
using DataLayer.Interfaces;
using DataLayer.Model;
using DataLayer.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
                Bio = user[0].Bio,
                Email = user[0].Email,
                Photo = user[0].Photo,
                UserName = user[0].UserName

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

 

        [HttpPost("login")]
        public IActionResult PostLoginUser(UserLoginModel userLoginModel)
        {
            var isLoggedIn = _dataService.Login(userLoginModel.UserName.Trim(), userLoginModel.Password);

            if (isLoggedIn)
            {
                return Ok("succes");
            }
            else
            {
                return BadRequest();
            }
        }
        





        [HttpPatch("edit")]
        public IActionResult EditUser(EditUserModel editUserModel)  
        {
            _dataService.EditUser(editUserModel.UserName, editUserModel.Photo, editUserModel.Bio, editUserModel.Email);
            return Ok($"Succesfully updated all information for {editUserModel.UserName}");
        }


        [HttpGet("bookmarks/{username}")]
        public IActionResult GetBookmarksFromUser(string username)
        {
         
            var result = _dataService.getBookmarksFromUser(username);

            List<TitlesModel> TitleModelList = new List<TitlesModel>();

            //
            if (result == null)
            {
                return NotFound();
            }

            foreach (var movie in result)
            {
                var titleID = movie.TitleId;
                var model = new TitlesModel
                {
                    URL = "http://localhost:5001/api/movies/" + titleID,
                    titleId = movie.TitleId,
                    TitleName = movie.TitleName,
                    Poster = movie.Poster,
                    TitlePlot = movie.TitlePlot,

                };
                TitleModelList.Add(model);
            }


            return Ok(_dataService.getBookmarksFromUser(username));
        }



        


        [HttpPost("rate")]
        public IActionResult PostRating(string username, string title_id, int rating)
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
        public IActionResult DeleteRating(string username, string title_id)
        {

             
                _dataService.DeleteMovieRating(username, title_id);
                return Ok($"Succesfully deleted rating of {title_id} on behalf of {username}");

        }

        [HttpGet("{username}/ratings")]
        public IActionResult GetRatingsForPerson(string username)
        {
            List<TitlesModel> RatingList = new List<TitlesModel>();

            var result = _dataService.GetRatingHistory(username);

            if (result == null)
            {
                return NotFound();
            }

            foreach (var movie in result)
            {

                var model = new TitlesModel
                {
                    URL = "http://localhost:5001/api/movies/" + movie.TitleId,
                    titleId = movie.TitleId,
                    TitleName = movie.TitleName,
                    Poster = movie.Poster

                };
                RatingList.Add(model);
            }
            return Ok(_dataService.GetRatingHistory(username));
        }


        [HttpGet("history/{username}")]
        public IActionResult getSearchForUser(string username)
        {

            List<SearchHistory> SearchHistory = new List<SearchHistory>();
            var result = _dataService.GetSearchHistories(username);
            Console.WriteLine(result);
            if (result == null)
            {
                return NotFound();
            }
            foreach (var movie in result)
            {
                var model = new SearchHistory
                {
                    Search = movie.Search
                };
                SearchHistory.Add(model);
            }
            return Ok(SearchHistory);
        }
    }
}

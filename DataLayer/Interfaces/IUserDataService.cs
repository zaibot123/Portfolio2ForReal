using DataLayer.Model;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    //:)
    public interface IuserDataService
    {
        void EditUser(string username, string bio, string photo, string email);
        IList<Password> Login(string username, string hashed_pass);
        void RateMovie(string username, string title_id, int rating);
        bool RegisterUser(string username, string password);

        void DeleteMovieRating(string username, string title_id);

        IList<RatingHistory> GetRatingHistory(string username);
        IList<User> GetSingleUser(string username);

        IList<User> GetAllUsers();
    }
}
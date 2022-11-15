using DataLayer.Model;
using System.Collections.Generic;
using DataLayer;

namespace DataLayer
{
    public interface IMovieDataService
    {
     
        IList<TitlesModel>? getTitles(string name);

        IList<TitlesModel>? GetSearch(string user_input);
        IList<TitlesModel>? getSimilarMovies(string user_input);
 
        IList<SearchResult>? getStructuredSearch(string title, string plot, string character, string name);
        IList<TitlesModel>? GetBestMatch(string user_input);
        IList<TitlesModel>? GetExcactSearch(string user_input);
        IList<WordModel>? GetWordToWord(string user_input);

        void AssignBookmark(string title_id, string username);
        void DeleteBookmark(string username, string title_id);
        void Bookmark(string username, string title_id);
        IList<MoviePageModel> GetSingleMovieByID(string ID);
    }
    public interface IActorDataService
    {
        IList<ActorsModel> getCoActors(string name);
        IList<ActorsModel>? getPopularActorsFromMovie(string title_id);
        IList<WordModel>? GetPersonWords(string user_input);
        IList<ActorsModel?> GetPersonSearch(string user_input);    
        IList<ActorsModel>? getStructuredPersonSearch(string name, string profession, string character);
        IList<ProfessionalsPageModel> GetSingleProfessionalFromID(string ID);
    }
//:)
    public interface ILoginDataService
    {
        IList<UserModel> EditUser(string username, string bio, string photo, string email);
        public IList<Password> Login(string username, string hashed_pass);
        void RateMovie(string username, string title_id, string rating);
        void RegisterUser(string username, string password);
    }
}
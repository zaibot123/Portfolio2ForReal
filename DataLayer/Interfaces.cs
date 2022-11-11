using DataLayer.Model;
using System.Collections.Generic;
using DataLayer;

namespace DataLayer
{
    public interface IMovieDataService
    {
     
        IList<TitlesModel>? getTitles(string name);

        IList<TitlesModel>? GetSearch(string user_input/*, int page, int pagesize*/);
        IList<TitlesModel>? getSimilarMovies(string user_input);
 
        IList<SearchResult>? getStructuredSearch(int page, int pagesize, string title, string plot, string character, string name);
        IList<TitlesModel>? GetBestMatch(string user_input);
        IList<TitlesModel>? GetExcactSearch(string user_input);
        IList<WordModel>? GetWordToWord(string user_input);
  
    }
    public interface IActorDataService
    {
        IList<ActorsModel> getCoActors(string name);
        IList<ActorsModel>? getPopularActorsFromMovie(string title_id);
        IList<WordModel>? GetPersonWords(string user_input);

    }
//:)
    public interface ILoginDataService
    {

        void RegisterUser(string username, string password);
    }
}
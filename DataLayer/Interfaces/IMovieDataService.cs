using DataLayer.Model;
using Nest;

namespace DataLayer.Interfaces
{
    public interface IMovieDataService
    {
        IList<Titles>? getTitles(string name);
        IList<Titles>? GetSearch(string user_input, int page, int pagesize);
        IList<TitleSimilarModel>? getSimilarMovies(string user_input);
        IList<SearchResult>? getStructuredSearch(string title, string plot, string character, string name);
        IList<Titles>? GetBestMatch(string user_input);
        IList<Titles>? GetExcactSearch(string user_input);
        IList<Word>? GetWordToWord(string user_input);

        //void AssignBookmark(string title_id, string username);
        //void DeleteBookmark(string username, string title_id);
        void Bookmark(string username, string title_id);
        void DeleteBookmark(string username, string title_id);
        IList<Titles> GetSingleMovieByID(string ID);
        int getSizeSimpleSearch(string user, string search);
    }
}
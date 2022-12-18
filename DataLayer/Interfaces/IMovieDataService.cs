using DataLayer.Model;
using Nest;
using System.Xml.Linq;

namespace DataLayer.Interfaces
{
    public interface IMovieDataService
    {
        IList<HasGenre>? getGenre(string prof_id);
        IList<Titles>? getTitles(string name);
        IList<Titles>? GetSearch(string username, string user_input, int page, int pagesize);
        IList<TitleSimilarModel>? getSimilarMovies(string user_input);
        IList<TitleSimilarModel>? getStructuredSearch(string title, string plot, string character, string name, int page, int pagesize);
        IList<Titles>? GetBestMatch(string user_input);
        IList<Titles>? GetExcactSearch(string user_input);
        IList<Word>? GetWordToWord(string user_input);
        void Bookmark(string username, string title_id);
        IList<PopularMovies> getPopularMovies();
        IList<Titles> GetSingleMovieByID(string ID);
        int getSizeSimpleSearch(string user, string search);
        void DeleteBookmark(string title_id, string username);
        int getSizeStructuredSearch(string title, string plot, string character, string name);
    }
}

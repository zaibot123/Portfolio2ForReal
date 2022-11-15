using DataLayer.Model;

namespace DataLayer.Interfaces
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
}
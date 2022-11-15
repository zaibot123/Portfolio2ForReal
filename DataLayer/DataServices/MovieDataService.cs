using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Nest;
using Npgsql;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;
using DataLayer.Interfaces;

namespace DataLayer
{
    public class MovieDataService : IMovieDataService
    {

        const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=1234";
        IList<Titles>? IMovieDataService.getTitles(string name)
        {
            using var db = new IMDBcontext();
            return db.Titles
                .Where(x => x.TitleName.Contains(name))
                .Select(x => new Titles
                {
                    TitleName = x.TitleName,

                    Poster = (x.Poster == null) ? "" : x.Poster
                })
                .ToList();
        }

        public IList<SearchResult>? getStructuredSearch(string title, string plot, string character, string name)
        {

            using var db = new IMDBcontext();
            var result = db.SearchResult.FromSqlInterpolated($"select * from structured_search({title}, {plot}, {character},{name})").ToList();
            return result;

        }

        public IList<Titles> GetSearch(string user_input)
        {
            var username = "Troels";
            using var db = new IMDBcontext();   
            var result = db.Titles.FromSqlInterpolated($"select * from simple_search({username},{user_input})").ToList();
            return result.OrderBy(x => x.TitleName).ToList();                
        }


        IList<Titles>? IMovieDataService.GetBestMatch(string user_input)
        {
            using var db = new IMDBcontext();
            string sqlstring = CreateSqlQueryForVariadic(user_input, "best_match");
            var result = db.Titles.FromSqlRaw(sqlstring).ToList();
            return result;
        }

        IList<Titles>? IMovieDataService.GetExcactSearch(string user_input)
        {
            using var db = new IMDBcontext();
            string sqlstring = CreateSqlQueryForVariadic(user_input, "excact_search");
            var result = db.Titles.FromSqlRaw(sqlstring).ToList();
            return result;
        }

        private static string CreateSqlQueryForVariadic(string user_input, string function_name)
        {
            var u = user_input.Split(",").Select(x => "'" + x + "'");
            var search = string.Join(",", u);
            var sqlstring = $"select * from {function_name}({search})";
            Console.WriteLine(sqlstring);
            return sqlstring;
        }

        IList<Titles>? IMovieDataService.getSimilarMovies(string title_id)
        {
            using var db = new IMDBcontext();
            var ResultList = new List<Titles>();
            using var connection = new NpgsqlConnection("host = localhost; db = imdb; uid = postgres; pwd = 1234");
            connection.Open();
            using var cmd = new NpgsqlCommand($"select * from similar_movies('{title_id}');", connection);

            // cmd.Parameters.AddWithValue("@query", "%ab%");
            using var reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                var actor = new Titles
                {
                    TitleName = reader.GetString(2),
                    Poster = reader.GetString(3)

                };
                ResultList.Add(actor);
            }
            return ResultList;

        }

        IList<Word>? IMovieDataService.GetWordToWord(string user_input)
        {
            using var db = new IMDBcontext();
            string sqlstring = CreateSqlQueryForVariadic(user_input, "word_to_word");
            var result = db.WordModel.FromSqlRaw(sqlstring).ToList();
            return result;
        }


        void IMovieDataService.AssignBookmark(string username, string title_id)
        {
            using var db = new IMDBcontext();
            var result = db.Bookmark.FromSqlInterpolated($"insert into bookmark(username, title_id) VALUES({username},{title_id}); select * from bookmark where username={username} and title_id = {title_id};").ToList();
        }

        void IMovieDataService.DeleteBookmark(string username, string title_id)
        {
            using var db = new IMDBcontext();
            //string sqlString 
            var result = db.Bookmark.FromSqlInterpolated($"delete from bookmark where username={username} and title_id = {title_id}; select * from bookmark;").ToList();
        }

        void IMovieDataService.Bookmark(string username, string title_id)
        {
            using var db = new IMDBcontext();
            //string sqlString 
            var result = db.Bookmark.FromSqlInterpolated($"Select * from bookmark_function({username}, {title_id})").ToList();
        }


        IList<Titles> IMovieDataService.GetSingleMovieByID(string ID)
        {
            using var db = new IMDBcontext();
            //string sqlString 
            var result = db.Titles.FromSqlInterpolated($"select * from title where title_id ={ID}").ToList();
            return result;
        }
    }
}
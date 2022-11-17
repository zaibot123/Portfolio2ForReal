using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Nest;
using Npgsql;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;
using DataLayer.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataLayer
{
    public class MovieDataService : IMovieDataService
    {

        const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=1234";
       public IList<Titles>? getTitles(string name)
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

        public IList<Titles> GetSearch(string user_input, int page,int pagesize)
        { 
            var username = "Troels";
            using var db = new IMDBcontext();
            Console.WriteLine($" page: {page}");
            var result = db.Titles.FromSqlInterpolated($"select * from simple_search({username},{user_input},{page},{pagesize})").ToList();
            return result.OrderBy(x => x.TitleName).ToList();                
        }


        public IList<Titles>? GetBestMatch(string user_input)
        {
            using var db = new IMDBcontext();
            string sqlstring = CreateSqlQueryForVariadic(user_input, "best_match");
            var result = db.Titles.FromSqlRaw(sqlstring).ToList();
            return result;
        }

       public IList<Titles>? GetExcactSearch(string user_input)
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

       public IList<TitleSimilarModel>? getSimilarMovies(string title_id)
        {
            using var db = new IMDBcontext();
            var result = db.TitlesSimilarModels.FromSqlInterpolated($"Select * from similar_movies({title_id})").ToList();
            return result;

        }

        public IList<Word>? GetWordToWord(string user_input)
        {
            using var db = new IMDBcontext();
            string sqlstring = CreateSqlQueryForVariadic(user_input, "word_to_word");
            var result = db.WordModel.FromSqlRaw(sqlstring).ToList();
            return result;
        }


        //public void AssignBookmark(string username, string title_id)
        //{
        //    using var db = new IMDBcontext();
        //    var result = db.Bookmark.FromSqlInterpolated($"insert into bookmark(username, title_id) VALUES({username},{title_id}); select * from bookmark where username={username} and title_id = {title_id};").ToList();
        //}

        //public void DeleteBookmark(string username, string title_id)
        //{
        //    using var db = new IMDBcontext();
        //    //string sqlString 
        //    var result = db.Bookmark.FromSqlInterpolated($"delete from bookmark where username={username} and title_id = {title_id}; select * from bookmark;").ToList();
        //}




        public void Bookmark(string username, string title_id)
        {
            using var db = new IMDBcontext();
            //string sqlString 
            var result = db.Bookmark.FromSqlInterpolated($"Select * from bookmark_function({username}, {title_id})").ToList();
        }


        public IList<Titles> GetSingleMovieByID(string ID)
        {
            using var db = new IMDBcontext();
            //string sqlString 
            var result = db.Titles.FromSqlInterpolated($"select * from title where title_id ={ID}").ToList();
            return result;
        }

        public int getSizeSimpleSearch (string user,string search)
        {
            using var db = new IMDBcontext();
            var con=(NpgsqlConnection)db.Database.GetDbConnection();
            con.Open();
            using var cmd = new NpgsqlCommand($"select simple_search_count('{user}','{search}')", con);
            return (int)cmd.ExecuteScalar();

        }

    }
}
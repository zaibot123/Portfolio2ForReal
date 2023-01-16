using DataLayer.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nest;
using Microsoft.EntityFrameworkCore;
using DataLayer.Security;
using DataLayer.Interfaces;

namespace DataLayer.DataServices
{
    public class UserDataService : IuserDataService
    {
       public IMDBcontext db = new IMDBcontext();

    public bool RegisterUser(string username, string password)
        {
            Authenticator auth = new Authenticator();
            bool registered = auth.register(username, password);
            if (registered)
            {
                Console.WriteLine("Registration succeeded");
                return true;
            }
            else
            {
                Console.WriteLine("Registration failed");
                return false;
            }

        }
        public IList<Titles> getBookmarksFromUser(string username)
        {
            //using var db = new IMDBcontext();
            var result = db.Titles.FromSqlInterpolated($"Select * from bookmark natural join title where username = {username}").ToList();
            return result;
        }

        public bool Login(string username, string password)
        {
            List<String> userNameHashedPassword = new List<String>();
            using var db = new IMDBcontext();

            Hashing hasher = new Hashing();

            var result = db.Password.FromSqlInterpolated($"select * from users where username = {username};").ToList();
            var hashed_password_from_user = hasher.hashSHA256(password.Trim(), result[0].Salt);

            Console.WriteLine(result[0].Salt + ": SALT");
            Console.WriteLine(result[0].HashedPassword + ": result[0] hashed password");
         
            Console.WriteLine(username + ": username IKKE hashed");
            Console.WriteLine(hashed_password_from_user + ": HASHED PASSWORD FROM USER");

            if (hashed_password_from_user == result[0].HashedPassword && username.Trim() == result[0].UserName.Trim())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EditUser(string username, string bio, string photo, string email)
        {
            var db = new IMDBcontext();
            db.Database.ExecuteSqlInterpolated($"select * from update_function({username},{bio}, {photo},{email});");
        }

        public void RateMovie(string username, string title_id, int rating)
        {
            var db = new IMDBcontext();
            db.Database.ExecuteSqlInterpolated($"select * from rating_function({username},{title_id}, {rating});");
        }

        public void DeleteMovieRating(string username, string title_id)
        {
            using var db = new IMDBcontext();
            var con = (NpgsqlConnection)db.Database.GetDbConnection();
            con.Open();
            using var cmd = new NpgsqlCommand($"select delete_rating_function('{username}','{title_id}')", con);
            cmd.ExecuteReader();
        }

        public IList<User> GetSingleUser(string username)
        {
            using var db = new IMDBcontext();
            var result = db.User.FromSqlInterpolated($"select * from users where username = {username};").ToList(); ;
            return result;
        }

        public IList<User> GetAllUsers()
        {
            using var db = new IMDBcontext();
            var result = db.User.FromSqlInterpolated($"select * from users;").ToList(); ;
            return result;
        }


        public IList<RatingHistory> GetRatingHistory(string username)
        {
            using var db = new IMDBcontext();
            var result = db.RatingHistory.FromSqlInterpolated($"Select username,title_id,title_name,rating,poster from rating_history natural join title where username={username} order by rating desc").ToList();
            return result;

        }


        public IList<SearchHistory> GetSearchHistories(string username)
        {
            Console.WriteLine("NOOO");
            using var db = new IMDBcontext();
            var result = db.searchHistories.FromSqlInterpolated($"select * from search_history where username={username}").ToList();
            return result;

        }

    }

}


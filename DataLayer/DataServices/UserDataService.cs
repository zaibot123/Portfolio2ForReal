using DataLayer.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secuurity;
using Nest;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DataServices
{
    public class UserDataService : ILoginDataService
    {

        void ILoginDataService.RegisterUser(string username, string password)
        {
            Authenticator auth = new Authenticator();
            bool registered = auth.register(username, password);
            if (registered) Console.WriteLine("Registration succeeded");
            else Console.WriteLine("Registration failed");
        }



        IList<Password> ILoginDataService.Login(string username, string hashed_pass)
        {

            using var db = new IMDBcontext();
            var result = db.Password.FromSqlInterpolated($"select * from password where username = {username} and hashed_password = {hashed_pass};").ToList();
            return result;
        }

        IList <UserModel> ILoginDataService.EditUser(string username, string bio, string photo, string email)
        {
            var db = new IMDBcontext();
            var result = db.UserModels.FromSqlInterpolated($"select * from update_function({username},{bio}, {photo},{email});").ToList();
            return result;

        }
    }
            

}


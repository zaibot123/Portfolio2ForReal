using DataLayer.Model;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    //:)
    public interface ILoginDataService
    {
        IList<UserModel> EditUser(string username, string bio, string photo, string email);
        public IList<Password> Login(string username, string hashed_pass);
        void RateMovie(string username, string title_id, string rating);
        void RegisterUser(string username, string password);
    }
}
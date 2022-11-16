using DataLayer.Model;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    //:)
    public interface ILoginDataService
    {
        void EditUser(string username, string bio, string photo, string email);
        IList<Password> Login(string username, string hashed_pass);
        void RateMovie(string username, string title_id, string rating);
        bool RegisterUser(string username, string password);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataServices
{
    public class LoginDataService : ILoginDataService
    {

        void ILoginDataService.RegisterUser(string username, string password)
        {
            Authenticator auth = new Authenticator();
            bool registered = auth.register(username, password);
            if (registered) Console.WriteLine("Registration succeeded");
            else Console.WriteLine("Registration failed");
        }

    }
}

// Authenticator.cs

using Npgsql;
using System.Runtime.Intrinsics.Arm;

namespace DataLayer.Security
{

    public class Authenticator
    {
        protected NpgsqlConnection? con;
        public Hashing hashing;

        public Authenticator()
        {
            try
            {
               
                string s = "host=cit.ruc.dk;db=cit03;uid=cit03;pwd=tAtc9jnYtgip;";
                con = new NpgsqlConnection(s);
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught in Authenticator.cs");
                Console.WriteLine("Could not connect to database");
                Console.WriteLine(e);
            }
            hashing = new Hashing();
        }

        public bool register(string username, string password)
        {
            // username can't be the empty string
            if (username.Length == 0)
            {
                Console.WriteLine("Username must contain at least one character");
                return false;
            }

            // check the password
            if (!passwordIsOK(password, username))
            {
                Console.WriteLine("Password is too weak");
                return false;
            }

            // obtain hash + salt
            Tuple<string, string> hs = hashing.hash(password);
            string hashedpassword = hs.Item1;
            string salt = hs.Item2;

            // add (username, salt, password) to table 'password'
            string sql = sqlInsertUserRecord(username, salt, hashedpassword);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught in Authenticator.cs");
                Console.WriteLine("Could not insert user record into table password");
                if (e.ToString().Contains("duplicate key"))
                {           // field username is primary key
                    Console.WriteLine("Username has been taken already");
                }
                return false;
            }
            // Instead of ExecuteNonQuery I could use ExecuteReader(),
            // but ExecuteNonQuery is more simple since there is no result set
            return true;
        }


        // check the password
        public virtual bool passwordIsOK(string password, string username)
        {
            if (password.Length >= 7 && !username.Contains(password)) return true;
            else return false;
        }

        // sqlSetUserRecord is used in register()
        virtual public string sqlInsertUserRecord(string username, string salt, string hashedpassword)
        {
            return "insert into users (username,salt,hashed_password) values ("
                             + "'" + username + "',"
                             + "'" + salt + "',"
                             + "'" + hashedpassword + "'"
                             + ")";
        }
    }
}


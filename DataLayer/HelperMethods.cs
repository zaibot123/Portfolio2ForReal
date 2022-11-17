using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Nest;
using Npgsql;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;

namespace DataLayer
{
    public class  HelperMethods
    {
        public  string CreateSqlQueryForVariadic(string user_input, string function_name)
        {
            var u = user_input.Split(",").Select(x => "'" + x + "'");
            var search = string.Join(",", u);
            var sqlstring = $"select * from {function_name}({search})";
            Console.WriteLine(sqlstring);
            return sqlstring;
        }

    }
}

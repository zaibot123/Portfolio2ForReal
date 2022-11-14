using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Password
    {
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string UserName { get; set; }


    }
}

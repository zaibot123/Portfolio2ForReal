using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Professionals
    {
        public string ProfId { get; set; }
        public string ProfName { get; set; }
        public int DeathYear { get; set; }
        public int BirthYear { get; set; }

        public double ProfRating { get; set; }
    }
}

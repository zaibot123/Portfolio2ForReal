using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class ProfessionalsPageModel
    {
        //public ProfessionalsPageModel()
        //{
        //    ProfRating = 0;
        //}

        public string ProfName { get; set; }
        public string? DeathYear { get; set; }
        public string? BirthYear { get; set; }
        public double? ProfRating { get; set; }
    }
}

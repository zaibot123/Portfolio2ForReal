using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer.Model
{
    public class PopularMovies
    {
        public double? AvgRating { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string? Poster { get; set; }
    }
}

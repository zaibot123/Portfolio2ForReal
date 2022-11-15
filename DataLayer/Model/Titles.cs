using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public  class Titles
    {

        public string TitleId { get; set; }
        public string TitleName { get; set; }
        public string? TitleType { get; set; }
        public string? Poster { get; set; }
        public string? TitlePlot { get; set; }
        public string? StartYear { get; set; }
        public string? EndYear { get; set; }
        public int? Runtime { get; set; }
        public bool? IsAdult { get; set; }
        public int? NrRatings { get; set; }
        public double? AvgRating { get; set; }


    }
}

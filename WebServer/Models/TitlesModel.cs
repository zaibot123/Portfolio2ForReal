using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class TitlesModel
    {
        public string URL { get; set; }

        public string titleId { get; set; }
        public string TitleName { get; set; }
        public string? Poster { get; set; } 

        public string? TitlePlot { get; set; }

        public string Username { set; get; }
        //
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class ProfessionalsModel
    {
        public string? URL { get; set; }
        public string  Name { get; set; }
        public string? DeathYear { get; set; }
        public string? BirthYear { get; set; }
  
        public double? ProfRating { get; set; }
        public string? Characters { get; set; }
        public string ID { get; set; }
    }
}

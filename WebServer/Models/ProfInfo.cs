using DataLayer.Model;

namespace WebServer.Models
{
    public class ProfInfo
    {

        public List<Characters>? Characters { get; set; }
        public List<Profession>? Professions { get; set; }
        public List<TitleName>? KnownFor { get; set; }

    
    }
}

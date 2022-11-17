using DataLayer.Model;
using Nest;

namespace DataLayer.Interfaces
{
    public interface IActorDataService
    {
        IList<Professionals> getCoActors(string name);
        IList<Word>? GetPersonWords(string user_input);
        IList<Professionals?> GetPersonSearch(string user_input);
        IList<Professionals>? getPopularActorsFromMovie(string title_id, int page, int pagesize);
        IList<Professionals>? getStructuredPersonSearch(string name, string profession, string character);
        Professionals? GetSingleProfessionalFromID(string ID);


    }
}
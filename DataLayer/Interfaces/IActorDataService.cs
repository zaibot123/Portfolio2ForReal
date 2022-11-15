using DataLayer.Model;

namespace DataLayer.Interfaces
{
    public interface IActorDataService
    {
        IList<Professionals> getCoActors(string name);
        IList<Professionals>? getPopularActorsFromMovie(string title_id);
        IList<Word>? GetPersonWords(string user_input);
        IList<Professionals?> GetPersonSearch(string user_input);
        IList<Professionals>? getStructuredPersonSearch(string name, string profession, string character);
        Professionals? GetSingleProfessionalFromID(string ID);


    }
}
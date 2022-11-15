using DataLayer.Model;

namespace DataLayer.Interfaces
{
    public interface IActorDataService
    {
        IList<ActorsModel> getCoActors(string name);
        IList<ActorsModel>? getPopularActorsFromMovie(string title_id);
        IList<WordModel>? GetPersonWords(string user_input);
        IList<ActorsModel?> GetPersonSearch(string user_input);
        IList<Professionals>? getStructuredPersonSearch(string name, string profession, string character);
        Professionals? GetSingleProfessionalFromID(string ID);


    }
}
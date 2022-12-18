using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace DataLayer.Interfaces
{
    public interface IActorDataService
    {

        IList<Word>? GetPersonWords(string user_input);
        IList<Professionals?> GetPersonSearch(string user_input);
        IList<Professionals>? getPopularActorsFromMovie(string title_id, int page, int pagesize);
        IList<Professionals>? getStructuredPersonSearch(string name, string profession, string character);
        Professionals? GetSingleProfessionalFromID(string ID);

        IList<Characters>? getCharacters(string prof_id);

        IList<TitleName>?getBestKnownFor(string prof_id);

        IList<Profession>? getProfessions(string prof_id);
        IList<SimpleProfessionals>? getCoActors(string actorId);

    }
}
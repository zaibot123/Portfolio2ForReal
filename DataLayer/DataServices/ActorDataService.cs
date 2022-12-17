using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Npgsql;
using DataLayer.Interfaces;
using Nest;

namespace DataLayer
{
    public class ActorDataService : IActorDataService
    {
            public IList<Professionals>? getCoActors(string actorname)
        {
            using var db = new IMDBcontext();
            var result = db.Professionals.FromSqlInterpolated($"select * from co_actors_function({actorname})").ToList();
            return result;
        }

        public IList<Professionals>? getPopularActorsFromMovie(string title_id, int page = 0, int pagesize = 10)
        {
            using var db = new IMDBcontext();
            var liste = db.Professionals.FromSqlInterpolated($"select * from populer_actors({title_id})");
            return liste
            .Skip(page * pagesize)
            .Take(pagesize)
            .OrderBy(x => x.ProfName)
            .ToList();
        }

        public IList<Word> GetPersonWords(string actorname)
        {
            using var db = new IMDBcontext();
            var result = db.WordModel.FromSqlInterpolated($"select * from person_words({actorname})").ToList();
            return result;
        }

        public IList<Professionals>? getStructuredPersonSearch(string name, string profession, string character)
        {
            using var db = new IMDBcontext();
            var result = db.Professionals.FromSqlInterpolated($"select * from structured_search_person({name},´{profession}, {character})").ToList();
            return result;

        }

        public IList<Professionals> GetPersonSearch(string user_input)
        {
           // var username = "Troels";
            using var db = new IMDBcontext();
            var result = db.Professionals.FromSqlInterpolated($"select * from simple_search_person({user_input})").ToList();
            return result;
        }

        public Professionals? GetSingleProfessionalFromID(string ID)
        {
            var username = "Troels";
            using var db = new IMDBcontext();
            return db.Professionals.FromSqlInterpolated($"select * from professionals where prof_id={ID}").FirstOrDefault();

        }
    }
}


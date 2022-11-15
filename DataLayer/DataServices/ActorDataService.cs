using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Npgsql;
using DataLayer.Interfaces;

namespace DataLayer
{
    public class ActorDataService : IActorDataService
    {

        const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=1234";
            public IList<ActorsModel>? getCoActors(string actorname)
        {
            using var db = new IMDBcontext();
            var result = db.ActorsModel.FromSqlInterpolated($"select * from co_actors_function({actorname})").ToList();
            return result;
        }

        public IList<ActorsModel>? getPopularActorsFromMovie(string title_id)
        {

            var ResultList = new List<ActorsModel>();
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            using var cmd = new NpgsqlCommand($"select * from populer_actors('{title_id}');", connection);

            // cmd.Parameters.AddWithValue("@query", "%ab%");
            using var reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                var actor = new ActorsModel
                {
                    ActorName = reader.GetString(0)
                };
                ResultList.Add(actor);
            }
            return ResultList;

        }
        //yesy
        public IList<WordModel> GetPersonWords(string actorname)
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

      

        public IList<ActorsModel> GetPersonSearch(string user_input)
        {
            var username = "Troels";
            using var db = new IMDBcontext();
            var result = db.ActorsModel.FromSqlInterpolated($"select * from simple_search_person({username},{user_input})").ToList();
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


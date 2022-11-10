using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Nest;
using Npgsql;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;

namespace DataLayer
{
    public class ActorDataService : IActorDataService
    {

        const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=Google-1234";


        IList<ActorsModel>? IActorDataService.getCoActors(string actorname)
        {
            using var db = new IMDBcontext();
            var ActorList = new List<ActorsModel>();
            using var connection = new NpgsqlConnection("host = localhost; db = imdb; uid = postgres; pwd = Google-1234");
            connection.Open();
            using var cmd = new NpgsqlCommand($"select * from co_actors_function('{actorname}');", connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var actor = new ActorsModel
                {
                    ActorName = reader.GetString(1)
                };
                ActorList.Add(actor);
            }
            return ActorList;
        }

        IList<ActorsModel>? IActorDataService.getPopularActorsFromMovie(string title_id)
        {

            var ResultList = new List<ActorsModel>();
            using var connection = new NpgsqlConnection("host = localhost; db = imdb; uid = postgres; pwd = Google-1234");
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

        IList<WordModel> IActorDataService.GetPersonWords(string actorname)
        {
            using var db = new IMDBcontext();
            var result = db.WordModel.FromSqlInterpolated($"select * from person_words({actorname})").ToList();
            return result;
        }


    }
}


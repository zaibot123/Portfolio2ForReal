using DataLayer;
using DataLayer.Interfaces;
using DataLayer.Model;
using DataLayer.Security;
using WebServer.Models;
using Xunit;



namespace Assignment4.Tests
{
    public class DataServiceTests
    {

        /* Categories */
        private IMovieDataService _movieDataService;
        private IActorDataService _actorDataService;
        private IuserDataService _userdataservice;

        [Fact]
        public void CrateEmptyActorsModelWithNullValue()
        {
            var ActorsModel = new ProfessionalsModel();
            Assert.Equal(null, ActorsModel.Name);
        }


        [Fact]
        public void CoActors()
        {
            var service = new ActorDataService();
            var result = service.getCoActors("Jennifer Aniston");
            var name = result.First().ProfName;
            Assert.Equal(10, result.Count);
            Assert.Equal("Courteney Cox", name);
        }



        [Fact]
        public void SimpleSearch()
        {
            var service = new MovieDataService();
            IList<Titles>? result = (IList<Titles>?)service.GetSearch("dog", 0, 10);
            var name = result.First().TitleName;
            Assert.Equal("10 jaar leuven kort", name);


        }


        [Fact]
        public void SimpleSearchPaging()
        {
            var service = new MovieDataService();
            IList<Titles>? result = (IList<Titles>?)service.GetSearch("dog", 1, 10);
            var name = result.First().TitleName;
            Assert.Equal("A Finished Life: The Goodbye & No Regrets Tour", name);

        }

        [Fact]
        public void LoginValid()
        {
            var x = _userdataservice.Login("Henrik", "F71AFC9F3FF38638EC539B8548A27AC97F0876732DD5E9CA0DF25BFB3EDF4D76");
            Assert.Equal(0,x.Count);
        }


        [Fact]
        public void LoginInvalid()
        {
            var x = _userdataservice.Login("Henrik", "F71AFC9F3FF38638EC539B8548A27AC97F087732DD5E9CA0DF25BFB3EDF4D76");
            Assert.Equal("False",x.ToString());


        }
    }
}

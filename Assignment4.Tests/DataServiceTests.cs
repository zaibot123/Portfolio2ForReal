using DataLayer;
using DataLayer.DataServices;
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
        private IUserDataService _userdataservice;

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
            var result = service.getCoActors("nm0000098");
            var name = result.First().Name;
            Assert.Equal(10, result.Count);
            Assert.Equal("Courteney Cox", name);
        }



        [Fact]
        public void SimpleSearch()
        {
            var service = new MovieDataService();
            IList<Titles>? result = (IList<Titles>?)service.GetSearch("Tobias","dog", 0, 10);
            var name = result.First().TitleName;
            Assert.Equal("10 jaar leuven kort", name);


        }


        [Fact]
        public void SimpleSearchPaging()
        {
            var service = new MovieDataService();
            IList<Titles>? result = (IList<Titles>?)service.GetSearch("Tobias","dog", 1, 10);
            var name = result.First().TitleName;
            Assert.Equal("A Dog's Purpose: A Writer's Purpose", name);

        }

        [Fact]
        public void LoginValid()
        {
            var service = new UserDataService();
            var x = service.Login("Tobias", "Google-1234");
            Assert.Equal(x, true);
        }


        [Fact]
        public void LoginInvalid()
        {
            var service = new UserDataService();
            var x = service.Login("Tobias", "HEJHEJ");
            Assert.False(x);
        }
    }
}

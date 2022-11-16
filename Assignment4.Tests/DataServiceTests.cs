//using DataLayer;
//using DataLayer.Interfaces;
//using DataLayer.Model;
//using DataLayer.Security;
//using WebServer.Models;
//using Xunit;



//namespace Assignment4.Tests
//{
//    public class DataServiceTests
//    {

//        /* Categories */
//        private IMovieDataService _movieDataService;
//        private IActorDataService _actorDataService;

//        [Fact]
//        public void CrateEmptyActorsModelWithNullValue()
//        {
//            var ActorsModel = new ProfessionalsModel();
//            Assert.Equal(null, ActorsModel.Name);
//        }


//        [Fact]
//        public void CoActors()
//        {
//            var service = new ActorDataService();
//            var result = service.getCoActors("Jennifer Aniston");
//            var name = result.First().ProfName;
//            Assert.Equal(10, result.Count);
//            Assert.Equal("Courteney Cox", name);

//        }



//        [Fact]
//        public void SimpleSearch()
//        {
//            var service = new MovieDataService();
//            IList<Titles>? result = (IList<Titles>?)service.GetSearch("dog",0,10);
//            var name = result.First().TitleName;
//            Assert.Equal("10 jaar leuven kort", name);


//        }


//        [Fact]
//        public void SimpleSearchPaging()
//        {
//            var service = new MovieDataService();
//            IList<Titles>? result = (IList<Titles>?)service.GetSearch("dog", 1, 10);
//            var name = result.First().TitleName;
//            Assert.Equal("A Finished Life: The Goodbye & No Regrets Tour", name);


//        }

//        [Fact]
//        public void LoginValid()
//        {
//            var service = new Authenticator();
//            bool result = service.login("Henrik", "henrik1234");
//            Assert.True(result);

//        }
//    }
//}


        //[Fact]
        //public void LoginInvalid()
        //{
        //    var service = new Authenticator();
        //    bool result = service.login("1234", "henrik1234");
        //    Assert.False(result);


        //}

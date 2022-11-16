using System;
using System.Net;
using System.Text;
using DataLayer.Model;
using DataLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Assignment4.Tests

{

    public class WebServiceTests
    {



        /* /api/categories */

        [Fact]
        public void SimpleSearchWithValidUrl()
        {
            var validsearchurl = "http://localhost:5001/api/movies?searchtype=simple&search=warrior";
            var (titles, statusCode) = GetObject(validsearchurl);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void SimilarWithInvalidID()
        {
            var invalidsimilarurl = "http://localhost:5001/api/movies/tt1514122/similar";
            var (similar, statusCode) = GetObject(invalidsimilarurl);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void SimilarWithValidID()
        {
            var validsimilarurl = "http://localhost:5001/api/movies/tt15141242/similar";
            var (similar, statusCode) = GetArray(validsimilarurl);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }



        [Fact]
        public void SuccesfulRegister()
        {
            using var db = new IMDBcontext();
            var con = (NpgsqlConnection)db.Database.GetDbConnection();
            con.Open();
            var cmd = new NpgsqlCommand($"DELETE FROM PASSWORD WHERE USERNAME ='test';", con);
            cmd.ExecuteReader();
            var validsimilarurl = "http://localhost:5001/api/register?username=test&password=testpassword";


            var (register, statusCode) = GetObject(validsimilarurl);

            Assert.Equal(HttpStatusCode.OK, statusCode);

        }

        [Fact]
        public void ValidLogin()
        {
            var validloginurl = "http://localhost:5001/api/user/login?username=Henrik&hashed_password=C0EB542D4EFFC7C41319AE19B6D28EC29FE0806F4318C77533D8AE323DC921ED";
            var (data, statusCode) = GetArray(validloginurl);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Henrik", data.First["userName"].ToString());
            Assert.Equal(1, data.Count);

        }




        [Fact]
        public void ApiCoActors()
        {
            const string ActorsApi = "http://localhost:5001/api/actors/Jennifer Aniston/coactors";
            var (data, statusCode) = GetArray(ActorsApi);



            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(10, data.Count);
            Assert.Equal("Courteney Cox", data.First()["name"].ToString());
            Assert.Equal("Ira Ungerleider", data.Last()["name"].ToString());
        }





        [Fact]
        public void PopularActorsInvalidID()
        {
            var invalidsimilarurl = "http://localhost:5001/actors/popular/tt1514122";

            var (register, statusCode) = GetObject(invalidsimilarurl);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

        }



        // Helpers

        (JArray, HttpStatusCode) GetArray(string url)
            {
                var client = new HttpClient();
                var response = client.GetAsync(url).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
            }

            (JObject, HttpStatusCode) GetObject(string url)
            {
                var client = new HttpClient();
                var response = client.GetAsync(url).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
            }

            (JObject, HttpStatusCode) PostData(string url, object content)
            {
                var client = new HttpClient();
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json");
                var response = client.PostAsync(url, requestContent).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
            }

            HttpStatusCode PutData(string url, object content)
            {
                var client = new HttpClient();
                var response = client.PutAsync(
                    url,
                    new StringContent(
                        JsonConvert.SerializeObject(content),
                        Encoding.UTF8,
                        "application/json")).Result;
                return response.StatusCode;
            }

            HttpStatusCode DeleteData(string url)
            {
                var client = new HttpClient();
                var response = client.DeleteAsync(url).Result;
                return response.StatusCode;
            }
     
    }
}




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
using WebServer.Models;
using DataLayer.DataServices;

namespace Assignment4.Tests

{

    public class WebServiceTests
    {



        /* /api/categories */

        [Fact]
        public void SimpleSearchWithValidUrl()
        {
            var validsearchurl = "http://localhost:5001/api/movies?searchtype=simple&username=Tobias&title=warrior";
            var (titles, statusCode) = GetObject(validsearchurl);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void SimilarWithInvalidID()
        {
            var invalidsimilarurl = "http://localhost:5001/api/movies/tt1514122/similar";
            var (data, statusCode) = GetObject(invalidsimilarurl);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void SimilarWithValidID()
        {

            var validsimilarurl = "http://localhost:5001/api/movies/tt15141242/similar";
            var (data, statusCode) = GetArray(validsimilarurl);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Pokiri", data.First["titleName"].ToString());
        }


        [Fact]
        public void ApiCoActors()
        {
            const string ActorsApi = "http://localhost:5001/api/actors/coactors/nm0000098";
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
            var (register, statusCode) = GetArray(invalidsimilarurl);
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




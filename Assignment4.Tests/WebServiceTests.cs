using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;


namespace Assignment4.Tests

{

    public class WebServiceTests
    {



        /* /api/categories */

        [Fact]
        public void SimpleSearchWithValidUrl()
        {
            var validsearchurl = "http://localhost:5001/movies?searchtype=simple&title=warrior";
            var (titles, statusCode) = GetArray(validsearchurl);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void SimilarWithInvalidID()
        {
            var invalidsimilarurl = "http://localhost:5001/movies/tt1514122/similar";
            var (similar, statusCode) = GetObject(invalidsimilarurl);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void SimilarWithValidID()
        {
            var validsimilarurl = "http://localhost:5001/movies/tt15141242/similar";
            var (similar, statusCode) = GetArray(validsimilarurl);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        public void ValidLogin() {
            var validloginurl = "http://localhost:5001/login?username=Henrik&hashed_password=C0EB542D4EFFC7C41319AE19B6D28EC29FE0806F4318C77533D8AE323DC921ED";
            var (data, statusCode) = GetArray(validloginurl);
            
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(1, data.Count);
            Assert.Equal("Henrik", data.First()["username"].ToString());




        }

        [Fact]
        public void ApiCoActors()
        {
            const string ActorsApi = "http://localhost:5001/actors/Jennifer Aniston/coactors";
            var (data, statusCode) = GetArray(ActorsApi);



            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(10, data.Count);
            Assert.Equal("Courteney Cox", data.First()["actorName"].ToString());
            Assert.Equal("Ira Ungerleider", data.Last()["actorName"].ToString());
        }


        //[Fact]
        //public void SuccesfulRegister()
        //{
        //    var validsimilarurl = "http://localhost:5001/login?username=test&password=testpassword";

        //    var (register, statusCode) = GetObject(validsimilarurl);
        //    Assert.Equal(HttpStatusCode.OK, statusCode);

        //}


        [Fact]
        public void PopularActorsInvalidID()
        {
            var invalidsimilarurl = "http://localhost:5001/actors/popular/tt1514122";

            var (register, statusCode) = GetObject(invalidsimilarurl);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

        }


        //        [Fact]
        //        public void ApiCategories_GetWithInvalidCategoryId_NotFound()
        //        {
        //            var (_, statusCode) = GetObject($"{CategoriesApi}/0");

        //            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //        }

        //        [Fact]
        //        public void ApiCategories_PostWithCategory_Created()
        //        {
        //            var newCategory = new
        //            {
        //                Name = "Created",
        //                Description = ""
        //            };
        //            var (category, statusCode) = PostData(CategoriesApi, newCategory);

        //            string id = null;
        //            if (category["id"] == null)
        //            {
        //                var url = category["url"].ToString();
        //                id = url.Substring(url.LastIndexOf('/') + 1);
        //            }
        //            else
        //            {
        //                id = category["id"].ToString();
        //            }

        //            Assert.Equal(HttpStatusCode.Created, statusCode);

        //            DeleteData($"{CategoriesApi}/{id}");
        //        }


        //        [Fact]
        //        public void ApiCategories_PutWithValidCategory_Ok()
        //        {

        //            var data = new
        //            {
        //                Name = "Created",
        //                Description = "Created"
        //            };
        //            var (category, _) = PostData($"{CategoriesApi}", data);

        //            string id = null;
        //            if (category["id"] == null)
        //            {
        //                var url = category["url"].ToString();
        //                id = url.Substring(url.LastIndexOf('/') + 1);
        //            }
        //            else
        //            {
        //                id = category["id"].ToString();
        //            }


        //            var update = new
        //            {
        //                Name = category["name"] + "Updated",
        //                Description = category["description"] + "Updated"
        //            };

        //            var statusCode = PutData($"{CategoriesApi}/{id}", update);

        //            Assert.Equal(HttpStatusCode.OK, statusCode);

        //            var (cat, _) = GetObject($"{CategoriesApi}/{id}");

        //            Assert.Equal(category["name"] + "Updated", cat["name"]);
        //            Assert.Equal(category["description"] + "Updated", cat["description"]);

        //            DeleteData($"{CategoriesApi}/{id}");
        //        }

        //        [Fact]
        //        public void ApiCategories_PutWithInvalidCategory_NotFound()
        //        {
        //            var update = new
        //            {
        //                Id = -1,
        //                Name = "Updated",
        //                Description = "Updated"
        //            };

        //            var statusCode = PutData($"{CategoriesApi}/-1", update);

        //            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //        }

        //        [Fact]
        //        public void ApiCategories_DeleteWithValidId_Ok()
        //        {

        //            var data = new
        //            {
        //                Name = "Created",
        //                Description = "Created"
        //            };
        //            var (category, _) = PostData($"{CategoriesApi}", data);

        //            string id = null;
        //            if (category["id"] == null)
        //            {
        //                var url = category["url"].ToString();
        //                id = url.Substring(url.LastIndexOf('/') + 1);
        //            }
        //            else
        //            {
        //                id = category["id"].ToString();
        //            }

        //            var statusCode = DeleteData($"{CategoriesApi}/{id}");

        //            Assert.Equal(HttpStatusCode.OK, statusCode);
        //        }

        //        [Fact]
        //        public void ApiCategories_DeleteWithInvalidId_NotFound()
        //        {

        //            var statusCode = DeleteData($"{CategoriesApi}/-1");

        //            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //        }

        //        /* /api/products */

        //        [Fact]
        //        public void ApiProducts_ValidId_CompleteProduct()
        //        {
        //            var (product, statusCode) = GetObject($"{ProductsApi}/1");

        //            Assert.Equal(HttpStatusCode.OK, statusCode);
        //            Assert.Equal("Chai", product["name"]);
        //            Assert.Equal("Beverages", product["category"]["name"]);
        //        }

        //        [Fact]
        //        public void ApiProducts_InvalidId_CompleteProduct()
        //        {
        //            var (_, statusCode) = GetObject($"{ProductsApi}/-1");

        //            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //        }

        //        [Fact]
        //        public void ApiProducts_CategoryValidId_ListOfProduct()
        //        {
        //            var (products, statusCode) = GetArray($"{ProductsApi}/category/1");

        //            Assert.Equal(HttpStatusCode.OK, statusCode);
        //            Assert.Equal(12, products.Count);
        //            Assert.Equal("Chai", products.First()["name"]);
        //            Assert.Equal("Beverages", products.First()["categoryName"]);
        //            Assert.Equal("Lakkalikööri", products.Last()["name"]);
        //        }

        //        [Fact]
        //        public void ApiProducts_CategoryInvalidId_EmptyListOfProductAndNotFound()
        //        {
        //            var (products, statusCode) = GetArray($"{ProductsApi}/category/1000001");

        //            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //            Assert.Equal(0, products.Count);
        //        }


        //        [Fact]
        //        public void ApiProducts_NameContained_ListOfProduct()
        //        {
        //            var (products, statusCode) = GetArray($"{ProductsApi}/name/em");

        //            Assert.Equal(HttpStatusCode.OK, statusCode);
        //            Assert.Equal(4, products.Count);
        //            Assert.Equal("NuNuCa Nuß-Nougat-Creme", products.First()["productName"]);
        //            Assert.Equal("Flotemysost", products.Last()["productName"]);
        //        }

        //        [Fact]
        //        public void ApiProducts_NameNotContained_EmptyListOfProductAndNotFound()
        //        {
        //            var (products, statusCode) = GetArray($"{ProductsApi}/name/CIT");

        //            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //            Assert.Equal(0, products.Count);
        //        }


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




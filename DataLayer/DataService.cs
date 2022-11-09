using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Nest;
using Npgsql;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;

namespace DataLayer
{
    public class DataService : IDataService
    {

        const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=1234";
        IList<TitlesModel>? IDataService.getTitles(string name)
        {
            using var db = new IMDBcontext();
            return db.Titles
                .Where(x => x.TitleName.Contains(name))
                .Select(x => new TitlesModel
                {
                    TitleName = x.TitleName,

                    Poster = (x.Poster == null) ? "" : x.Poster
                })
                .ToList();
        }

        IList<ActorsModel>? IDataService.getCoActors(string name)
        {
            using var db = new IMDBcontext();
            var ActorList = new List<ActorsModel>();
            using var connection = new NpgsqlConnection("host = localhost; db = imdb; uid = postgres; pwd = 1234");
            connection.Open();

            using var cmd = new NpgsqlCommand($"select * from co_actors_function('{name}');", connection);

            // cmd.Parameters.AddWithValue("@query", "%ab%");
            using var reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                Console.WriteLine("!!");
                var actor = new ActorsModel
                {
                    ActorName = reader.GetString(1)
                };
                ActorList.Add(actor);
            }
            return ActorList;
        }





        IList<TitlesModel>? IDataService.GetSearch(string user_input)
        {

            var username = "Troels";
            using var db = new IMDBcontext();
            var result = db.TitlesModel.FromSqlInterpolated($"select * from simple_search({username},{user_input})").ToList();
            return result;

        }


        IList<TitlesModel>? IDataService.getSimilarMovies(string title_id)
        {

            using var db = new IMDBcontext();
            var ResultList = new List<TitlesModel>();
            using var connection = new NpgsqlConnection("host = localhost; db = imdb; uid = postgres; pwd = 1234");
            connection.Open();

            using var cmd = new NpgsqlCommand($"select * from similar_movies('{title_id}');", connection);

            // cmd.Parameters.AddWithValue("@query", "%ab%");
            using var reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                var actor = new TitlesModel
                {
                    TitleName = reader.GetString(2),
                    Poster = reader.GetString(3)

                };
                ResultList.Add(actor);
            }
            return ResultList;

        }

        IList<ActorsModel>? IDataService.getPopularActorsFromMovie(string title_id)
        {

            var ResultList = new List<ActorsModel>();
            using var connection = new NpgsqlConnection("host = localhost; db = imdb; uid = postgres; pwd = 1234");
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


        IList<SearchResult>? IDataService.getStructuredSearch(string title, string plot, string character, string name)
        {
 
            using var db = new IMDBcontext();
            var result = db.SearchResult.FromSqlInterpolated($"select * from structured_search({title}, {plot}, {character},{name})").ToList();
            return result;

        }


        void IDataService.AddSearch(string search)
        {
            throw new NotImplementedException();
        }

        /*
        IQueryable<SearchResult>? IDataService.getStructuredSearch(string title, string plot, string characters, string actorname)
        {
            Console.WriteLine(title);
            var ResultList = new List<SearchResult>();
            using (var db = new IMDBcontext())
            {
                var sqlstring = $"select * from structured_search('{title}','{plot}','{characters}','{actorname}')";
                var result = db.SearchResult.FromSqlInterpolated($"select * from structured_search('{title}','{plot}','{characters}','{actorname}')");
            }
             
                Console.WriteLine(result.ToString());
                Console.WriteLine(result.GetType());
             
                foreach (var searchResult in result)
                {
                    var search = new SearchResult
                    {

                        ActorNames = searchResult.ActorNames,
                        Title = searchResult.Title,
                        Characters = searchResult.Characters,
                        Plot = searchResult.Plot
                    };
                    Console.WriteLine($"her er search {search}");
                    ResultList.Add(search);
                }

                return result;
            
        }
         */
    }

}







/*
    public IList<Category> GetCategories()
        {
            using var db = new NorthwindContext();

            return db.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            using var db = new NorthwindContext();
            return db.Categories.Find(id);
        }

        public Category CreateCategory(string name, string description)
        {
            using var db = new NorthwindContext();
            var cat = new Category();
            cat.Id = GetCategories().Max(x => x.Id + 1);
            cat.Name = name;
            cat.Description = description; 
            db.Categories.Add(cat);
            db.SaveChanges();

            return cat;
        }

        public void CreateCategory(Category category)
        {
            using var db = new NorthwindContext();
            category.Id = GetCategories().Max(x => x.Id + 1);
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public bool DeleteCategory(int id)
        {
            using var db = new NorthwindContext();
            var cat = GetCategory(id);
            if (cat != null)
            {
                db.Categories.Remove(cat);
                db.SaveChanges();
                return true;
            }
            else return false;
  
        }

        public bool UpdateCategory(int id, string name, string desc)
        {
            using var db = new NorthwindContext();
            var cat = db.Categories.Find(id);
   
            if (cat != null)
            {
                cat.Description = desc;
                cat.Name = name;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Product? GetProduct(int id)
        {
            using var db = new NorthwindContext();
            return db.Products
                .Include(x=>x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public IList<ProductModel> GetProductByCategory(int id)
        {
            using var db = new NorthwindContext();
            return db.Products
                .Include(x => x.Category)
                .Where(x => x.Category.Id == id)
                .Select(x=> new ProductModel
                {
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    CategoryName = x.Category.Name
                })
                .ToList();
        }

        public IList<ProductModel> GetProductByName(string name)
        {
            using var db = new NorthwindContext();
            return db.Products
                .Where(x => x.Name.Contains(name))
                .Select(x => new ProductModel
                {
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    ProductName = x.Name
                })
                .ToList();
        }


        public Order? GetOrder(int id)
        {
            using var db = new NorthwindContext();

            return db.Orders
                .Include(x=>x.OrderDetails)
                .ThenInclude(x=>x.Product)
                .ThenInclude(x=>x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

       public IList<Order> GetOrders()
        {
            using var db = new NorthwindContext();
            return db.Orders.ToList();
        }



        public IList<OrderDetailsModel> GetOrderDetailsByOrderId(int id)
        {
            using var db = new NorthwindContext();
            return db.OrderDetails.Include(x => x.Product)
                .Where(x => x.OrderId == id)
                .Select(x => new OrderDetailsModel
                {
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Product = x.Product

                })
                .ToList();
        }

        public IList<OrderDetailsModel> GetOrderDetailsByProductId(int id)
        {
            var db = new NorthwindContext();

            return db.OrderDetails
               .Include(x => x.Order)
               .Where(x => x.ProductId == id)
               .Select(x => new OrderDetailsModel
               {
                   Quantity = x.Quantity,
                   UnitPrice = x.UnitPrice,
                   Order = x.Order
               }).OrderBy(x=>x.Order.Id)
               .ToList();

        }


        public bool UpdateCategory(Category category)
        {
            using var db = new NorthwindContext();
            var cat = db.Categories.Find(category.Id);

            if (cat != null)
            {
                cat.Description = category.Description;
                cat.Name = category.Name;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IList<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
*/
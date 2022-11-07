using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Nest;
using Npgsql;
using System.Xml.Linq;

namespace DataLayer
{
    public class DataService : IDataService
    {


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

        IList<TitlesModel>? IDataService.getCoActors(string name)
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


     IList<Search>? IDataService.getSearch()
        {
            using var db = new IMDBcontext();
            return db.Search
                .Select(x => new Search
                {
                    SearchString = x.SearchString,
                    Username = x.Username,
                })
                .ToList();
        }
        public void AddSearch(string search, string username="Default")
        {
          
            using var connection = new NpgsqlConnection("host = localhost; db = imdb; uid = postgres; pwd = 1234");
            connection.Open();
            using var cmd = new NpgsqlCommand($"INSERT INTO search_history (username,search_string) VALUES ({username},{search});", connection);
            // cmd.Parameters.AddWithValue("@query", "%ab%");
            using var reader = cmd.ExecuteReader();
        }
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
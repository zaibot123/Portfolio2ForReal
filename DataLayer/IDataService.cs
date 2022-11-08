using DataLayer.Model;
using System.Collections.Generic;
using DataLayer;

namespace DataLayer
{
    public interface IDataService
    {
     
        IList<TitlesModel>? getTitles(string name);
        void AddSearch(string search);
        IList<ActorsModel> getCoActors(string name);
        IList<TitlesModel>? getSearch(string user_input);
        //IList<Category> GetCategories();
        //Category? GetCategory(int id);
        //IList<Product> GetProducts();
        //Product? GetProduct(int id);
        //IList<ProductModel> GetProductByCategory(int id);
        //IList<ProductModel> GetProductByName(string name);
        //void CreateCategory(Category category);
        //bool UpdateCategory(Category category);
        //bool DeleteCategory(int id);
        //Order? GetOrder(int id); 

        // IList<ProductSearchModel> GetProductByName(string search);
    }
}
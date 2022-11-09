using DataLayer.Model;
using System.Collections.Generic;
using DataLayer;

namespace DataLayer
{
    public interface IDataService
    {
     
        IList<TitlesModel>? getTitles(string name);
        void AddSearch(string search);

       // IQueryable<SearchResult>? UseEntityFramework();
        IList<ActorsModel> getCoActors(string name);
        IList<TitlesModel>? GetSearch(string user_input);
        IList<TitlesModel>? getSimilarMovies(string user_input);
        IList<ActorsModel>? getPopularActorsFromMovie(string title_id);
      //  IQueryable<SearchResult>? UseEntityFramework(string name, string plot, string character, string title);
        IList<SearchResult>? getStructuredSearch(string title, string plot, string character, string name);
        IList<TitlesModel>? GetBestMatch(string user_input);
        IList<TitlesModel>? GetExcactSearch(string user_input);
        IList<WordModel>? GetWordToWord(string user_input);

        IList<WordModel>? GetPersonWords(string user_input);

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
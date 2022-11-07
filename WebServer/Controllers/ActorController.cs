using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer;
using WebServer.Models;
using DataLayer;
using DataLayer.Model;
using Npgsql;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace WebServer.Controllers
{


    [Route("coactors")]
    [ApiController]

    public class ActorController : ControllerBase
    {
        private IDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public ActorController(IDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }


        [HttpGet("{name}")]
        public IActionResult getCoactors(string name)
        {

            var ActorList = new List<string>();
            Console.WriteLine("Plain ADO stored procedure");
            using var connection = new NpgsqlConnection("host = localhost; db = imdb; uid = postgres; pwd = 1234");
            connection.Open();

            using var cmd = new NpgsqlCommand($"select * from co_actors_function('{name}');", connection);

            // cmd.Parameters.AddWithValue("@query", "%ab%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("!!");
                ActorList.Add($" {reader.GetString(1)}");
     
            }
            return Ok(ActorList);
        }




        /*
        [Route("api/categories")]
        [ApiController]
        public class CategoriesController : ControllerBase
        {
            private IDataService _dataService;
            private readonly LinkGenerator _generator;
            private readonly IMapper _mapper;

            public CategoriesController(IDataService dataService, LinkGenerator generator, IMapper mapper)
            {
                _dataService = dataService;
                _generator = generator;
                _mapper = mapper;
            }

            [HttpGet]
            public IActionResult GetCategories()
            {
                var categories =
                    _dataService.GetCategories().Select(x => CreateCategoryModel(x));
                return Ok(categories);
            }

            [HttpGet("{id}", Name = nameof(GetCategory))]
            public IActionResult GetCategory(int id)
            {
                var category = _dataService.GetCategory(id);

                if (category == null)
                {
                    return NotFound();
                }

                var model = CreateCategoryModel(category);

                return Ok(model);

            }

            [HttpPost]
            public IActionResult CreateCategory(CategoryCreateModel model)
            {
                var category = _mapper.Map<Category>(model);

                _dataService.CreateCategory(category);

                return CreatedAtRoute(null, CreateCategoryModel(category));
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteCategory(int id)
            {
                var deleted = _dataService.DeleteCategory(id);

                if (!deleted)
                {
                    return NotFound();
                }

                return Ok();
            }

            [HttpPut("{id}")]
            public IActionResult PutData(int id, Category category)
            {

                category.Id = id;   

                var updated = _dataService.UpdateCategory(category);

                if (!updated)
                {
                    return NotFound();
                }

                return Ok();
            }


            private CategoryModel CreateCategoryModel(Category category)
            {
                var model = _mapper.Map<CategoryModel>(category);
                model.Url = _generator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id });
                return model;
            }
        }
        */
    }
}
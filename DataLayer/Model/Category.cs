namespace DataLayer.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }


    //public class CategoryCreateModel
    //{
    //    public string? Name { get; set; }
    //    public string? Description { get; set; }
    //}

    public class ProductListModel
    {
        public string? Url { get; set; }
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
    }

    public class CategoryModel
    {
        public string? Url { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}



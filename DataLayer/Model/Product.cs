namespace DataLayer.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public int UnitPrice { get; set; }   

        public string QuantityPerUnit { get; set; }

        public int UnitsInStock { get; set; }   

    }
}

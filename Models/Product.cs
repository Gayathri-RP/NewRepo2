namespace SalesProject_Backend.Models
{
    
        public class Product
        {
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Discount { get; set; }
    }
}

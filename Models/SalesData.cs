namespace SalesProject_Backend.Models
{
    public class SalesData
    {
        public int OrderID { get; set; }
        public string ProductID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public int QuantitySold { get; set; }
        public decimal ShippingCost { get; set; }
        public string PaymentMethod { get; set; }
    }
}

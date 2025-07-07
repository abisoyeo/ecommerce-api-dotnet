namespace EcommerceApiSnapnetTestApp.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }  // Foreign key
        public List<ProductsModel> Products { get; set; } = new List<ProductsModel>();  // List of products in the order
        public bool Status { get; set; }  // E.g., true = shipped, false = pending
        public DateTime OrderDate { get; set; }  // Date when the order was placed

    }
}

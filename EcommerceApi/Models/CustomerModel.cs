using System.ComponentModel.DataAnnotations;

namespace EcommerceApiSnapnetTestApp.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public ICollection<Orders> CustomerOrders { get; set; } = new List<Orders>();
        [Required]
        public string ShippingAddress { get; set; }

    }


}

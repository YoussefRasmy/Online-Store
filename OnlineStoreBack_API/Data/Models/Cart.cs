using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public class Cart
	{
		public int Id { get; set; }
		public double TotalPrice { get; set; }
		[ForeignKey("Customer")]
		public string CustomerId { get; set; }



		//navegation prop
		public virtual ICollection<ProductCart> ProductCarts { get; set; }
		public virtual Customer Customer { get; set; }
		//public virtual ICollection<Product> Products { get; set; }	Will Creat new Table 
	}
}

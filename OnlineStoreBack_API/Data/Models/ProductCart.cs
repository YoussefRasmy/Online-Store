using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public class ProductCart
	{
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		[ForeignKey("Cart")]
		public int CartId { get; set; }
		public int Quantity { get; set; }
		public double TotalPrice { get; set; }

		//Nav Prop
		public virtual Product Product  { get; set; }
		public virtual Cart Cart  { get; set; }
	}
}

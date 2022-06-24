using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public class ProductOrder
	{
		[ForeignKey("Order")]
		public int OrderId { get; set; }
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public double TotalPrice { get; set; }




		public virtual Order Order { get; set; }
		public virtual Product Product { get; set; }
	}
}

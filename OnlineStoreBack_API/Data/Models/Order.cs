using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public enum _OrderState
	{
		Pending,
		Purches,
		OnItsWay,
		Delivered
	}

	public enum PaymentMethod
	{
		Visa,
		OnDeliver
	}
	public class Order
	{
		public int Id { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		//public Double shipping_Price { get; set; }
		public string Address { get; set; }
		public DateTime Order_Date { get; set; }
		public DateTime Deliver_Date { get; set; }
		//public _OrderState OrderState { get; set; }
		public double TotalPrice { get; set; }
		[Required]
		[ForeignKey("User")]
		public string UserId { get; set; }
		//public Double Total_Price { get; set; }

		//nav prop		
		public virtual StoreUser User { get; set; }
		public virtual ICollection<ProductOrder> ProductOrders { get; set; }


	}
}

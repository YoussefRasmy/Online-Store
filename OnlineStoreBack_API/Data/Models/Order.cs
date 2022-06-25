namespace OnlineStoreBack_API.Data.Models
{
	public enum _OrderState
	{
		Pending,
		Purches,
		OnItsWay,
		Delivered
	}
	public class Order
	{
		public int Id { get; set; }
		public Double shipping_Price { get; set; }
		public string Address { get; set; }
		public DateTime Order_Date { get; set; }
		public DateTime Deliver_Date { get; set; }
		public _OrderState OrderState { get; set; }
		//public Double Total_Price { get; set; }

		//nav prop		



	}
}

using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.DTO.ModelsDTO
{
	public class OrderDTO
	{
		public int id { get; set; }
		public int? OrderNum { get; set; }
		public string DeliveryAddress { get; set; }
		public double TotalPrice { get; set; }
		public PaymentMethod _PaymentMethod { get; set; }

		public List<ProductOrderReadDTO> products { get; set; }=new List<ProductOrderReadDTO>();














	}
}

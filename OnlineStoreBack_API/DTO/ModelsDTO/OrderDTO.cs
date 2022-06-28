namespace OnlineStoreBack_API.DTO.ModelsDTO
{
	public class OrderDTO
	{
		public int OrderNum { get; set; }
		public string DeliveryAddress { get; set; }
		public double TotalPrice { get; set; }
		public List<ProductOrderReadDTO> products { get; set; }














	}
}

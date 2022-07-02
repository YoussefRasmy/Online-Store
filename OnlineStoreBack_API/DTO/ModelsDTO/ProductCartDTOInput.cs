using System.ComponentModel.DataAnnotations;

namespace OnlineStoreBack_API.DTO.ModelsDTO
{
	public class ProductCartDTOInput
	{
		[Required]
		public int ProductId { get; set; }
		[Required]
		public int Quantity { get; set; }
	}
}

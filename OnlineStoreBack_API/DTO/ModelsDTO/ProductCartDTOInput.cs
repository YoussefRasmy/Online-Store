using System.ComponentModel.DataAnnotations;

namespace OnlineStoreBack_API.DTO.ModelsDTO;

public class ProductCartDTOInput
{
	public int ProductId { get; set; }
	public int Quantity { get; set; }
}

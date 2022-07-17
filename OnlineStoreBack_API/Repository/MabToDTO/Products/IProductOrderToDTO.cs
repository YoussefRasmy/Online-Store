using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository.MabToDTO
{
	public interface IProductOrderToDTO
	{
		List<ProductOrderReadDTO> ChangeToManyDTOs(List<ProductOrder> productOrders);
	}
}

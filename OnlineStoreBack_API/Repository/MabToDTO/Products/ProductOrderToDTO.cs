using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository.MabToDTO
{
	public class ProductOrderToDTO : IProductOrderToDTO
	{
		public ProductOrderToDTO()
		{

		}
		public List<ProductOrderReadDTO> ChangeToManyDTOs(List<ProductOrder> productOrders)
		{

			List<ProductOrderReadDTO> res = new List<ProductOrderReadDTO>();
			return res;	
			//foreach (var item in productOrders)
			//{
			//	res.Add(new ProductOrderReadDTO
			//	{
			//		ProductId = item.ProductId,
			//		Price = item.pr
			//	}
			//}
		}
	}
}

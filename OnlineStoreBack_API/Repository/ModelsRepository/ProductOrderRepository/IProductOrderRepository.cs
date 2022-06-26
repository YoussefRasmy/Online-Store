using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public interface IProductOrderRepository
	{
		List<ProductOrder> GetAllByOrderId(int orderId);
		ProductOrder GetById(int orderId, int productId);
		void Delete(int orderId, int productId);
		void Update(ProductOrder productOrder);
	}
}

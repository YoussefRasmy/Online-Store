using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public interface IOrderRepository : IGenericRepository<Order,int>
	{
		List<Order> GetAllByCurrentUserId();
		List<Order> GetAllByUserId(string userId);
		//void AddProducts();
	}
}

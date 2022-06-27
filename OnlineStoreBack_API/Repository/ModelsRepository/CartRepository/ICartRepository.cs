using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public interface ICartRepository : IGenericRepository<Cart, int>
	{
		Cart GetByCurrerntUserId();
		Cart GetBytUserId(string userId);

		void AddToCart (ProductCart productCart);

		void suptrackFromCart(ProductCart productCart);
		void TransfairToOrder(string address, DateTime deliverDate);


	}
}

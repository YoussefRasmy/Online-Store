using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public interface ICartRepository : IGenericRepository<Cart, int>
	{
		Cart GetByCurrerntUserId();
		Cart GetBytUserId(string userId);

		int AddToCart (ProductCart productCart);

		void suptrackFromCart(ProductCart productCart);
		int TransfairToOrder(string address, Cart cart, int paymentMethod,ref string errorMessage);
		//void CalculateCart(Cart cart);

	}
}

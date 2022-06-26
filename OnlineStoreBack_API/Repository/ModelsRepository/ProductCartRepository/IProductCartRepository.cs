using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public interface IProductCartRepository
	{
		List<ProductCart> GetAllByCartId (int cartId);
		ProductCart GetById (int cartId,int productId);
		void Delete(int cartId, int productId);
		void Update(ProductCart cart);

	}
}
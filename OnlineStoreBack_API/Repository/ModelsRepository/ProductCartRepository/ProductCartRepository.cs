using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public class ProductCartRepository : IProductCartRepository
	{
		private readonly OnlineStoreContext db;

		public ProductCartRepository(OnlineStoreContext context)
		{
			this.db = context;
		}

	

		public void Delete(int cartId, int productId)
		{
			var productCart = db.CartProducts.FirstOrDefault(x=>x.CartId == cartId && x.ProductId == productId);
			if (productCart != null)
			{
				db.CartProducts.Remove(productCart);
				db.SaveChanges();

			}

		}

		public List<ProductCart> GetAllByCartId(int cartId)
		{
			var res = db.CartProducts.Where(x=> x.CartId == cartId).ToList();
			return res;
		}

		public ProductCart GetById(int cartId, int productId)
		{
			return db.CartProducts.FirstOrDefault(x=>x.CartId==cartId && x.ProductId == productId);
		}

		

		public void Update(ProductCart cart)
		{
			throw new NotImplementedException();
		}
	}
}

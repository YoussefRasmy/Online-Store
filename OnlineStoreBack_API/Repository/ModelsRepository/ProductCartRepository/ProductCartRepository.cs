using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public class ProductCartRepository : IProductCartRepository
	{
		private readonly OnlineStoreContext db;
		private readonly IProductRepository productRepository;

		public ProductCartRepository(OnlineStoreContext context,IProductRepository productRepository)
		{
			this.db = context;
			this.productRepository = productRepository;
		}

		public void CalculatePrice(ProductCart cart)
		{
			var product = productRepository.GetById(cart.ProductId);
			cart.TotalPrice = cart.Quantity * product.Price;
		}

		public void ClearCart(int cartId)
		{
			var productCarts = GetAllByCartId(cartId);
			db.CartProducts.RemoveRange(productCarts);
			var cart = db.Carts.FirstOrDefault(c => c.Id == cartId);
			cart.TotalPrice = 0;
			db.SaveChanges();
			
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

using Microsoft.EntityFrameworkCore;
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

		public void AddProductCart(ProductCart productCart)
		{
			db.CartProducts.Add(productCart);
			db.SaveChanges();
		}

		public void CalculatePrice(ProductCart cart)
		{
			var product = productRepository.GetById(cart.ProductId);
			
		}

		public void ClearCart(int cartId)
		{
			var productCarts = GetAllByCartId(cartId);
			db.CartProducts.RemoveRange(productCarts);
			var cart = db.Carts.FirstOrDefault(c => c.Id == cartId);
			
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
			var res = db.CartProducts.Include(x=>x.Product).Where(x=> x.CartId == cartId).ToList();
			return res;
		}

		public ProductCart GetById(int cartId, int productId)
		{
			return db.CartProducts.FirstOrDefault(x=>x.CartId==cartId && x.ProductId == productId);
		}

		

		public void Update(ProductCart cart)
		{
			
		}
	}
}

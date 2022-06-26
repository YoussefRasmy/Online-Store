using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public class ProductOrderRepository : IProductOrderRepository
	{
		private readonly OnlineStoreContext db;

		public ProductOrderRepository(OnlineStoreContext context)
		{
			this.db = context;
			
		}
		public void Delete(int orderId, int productId)
		{
			var productOrder = db.OrderProducts.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
			if (productOrder != null)
			{
				db.OrderProducts.Remove(productOrder);
				db.SaveChanges();

			}

		}

		public List<ProductOrder> GetAllByOrderId(int orderId)
		{
			var res = db.OrderProducts.Where(x => x.OrderId == orderId).ToList();
			return res;
		}

		public ProductOrder GetById(int orderId, int productId)
		{
			return db.OrderProducts.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
		}

		public void Update(ProductOrder productOrder)
		{
			throw new NotImplementedException();
		}

	}
}

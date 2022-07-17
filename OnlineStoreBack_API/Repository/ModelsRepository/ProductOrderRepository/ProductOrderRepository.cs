using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public class ProductOrderRepository : IProductOrderRepository
	{
		private readonly OnlineStoreContext db;
		private readonly IProductRepository productRepository;

		public ProductOrderRepository(OnlineStoreContext context,IProductRepository productRepository)
		{
			this.db = context;
			this.productRepository = productRepository;
		}

		public void Add(ProductOrder productOrder)
		{
			db.OrderProducts.Add(productOrder);
			var outQuantity = productOrder.Quantity;
			//inventory update
			productRepository.DecreacInventorty(productOrder.ProductId, outQuantity);
			db.SaveChanges();
		}

		public void AddList(List<ProductOrder> productOrderList)
		{
			db.OrderProducts.AddRange(productOrderList);
			foreach (var item in productOrderList)
			{
				productRepository.DecreacInventorty(item.ProductId, item.Quantity);

			}
			db.SaveChanges();
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
			db.SaveChanges();
		}

	}
}

using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository

{
	public class ProductRepository : GenericRepository<Product, int>, IProductRepository
	{
		private readonly OnlineStoreContext db;

		public ProductRepository(OnlineStoreContext context) : base(context)
		{
			this.db = context;
		}

		public List<Product> GetAllSortByEnName	(string EnglishName)
		{
			var Products= db.Products.OrderByDescending(x => x.EnglishName).ToList();
			return Products;
		}

		public void DecreacInventorty(int id, int count)
		{
			var product = db.Products.FirstOrDefault(x => x.Id == id);
			if (product != null)
			{
				product.Quantity -= count;
			}
		}
		public void IncreacInventory(int id, int count)
		{
			var product = db.Products.FirstOrDefault(x => x.Id == id);
			if (product != null)
			{
				product.Quantity += count;
			}
		}

	} 
}

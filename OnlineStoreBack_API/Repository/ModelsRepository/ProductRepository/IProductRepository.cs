using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository
{
	public interface IProductRepository:IGenericRepository<Product,int>
	{
		#region ADD
		//public int AddProduct(ProductDTO product);
		#endregion

		#region GET

		//Get all
		//public List<Product> GetAllProducts();
		//Get by Name sorting by Product English name and grouping by Product Category
		public List<Product> GetAllSortByEnName(string EnglishName);
		//Get by Id
		//public Product GetProductByID(int ProductId);
		#endregion

		#region UPDATE
		void IncreacInventory(int id, int count);
		void DecreacInventorty(int id, int count);
		#endregion

		#region DELETE
		//public void DeleteProductByID(int productId);

		#endregion

	}
}

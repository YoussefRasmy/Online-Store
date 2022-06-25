using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public interface ICategory
	{
		#region ADD
		public int AddCategory(string categoryName,int? paretnCategoryId);
		#endregion

		#region GET

		//Get all
		public List<Category> GetAllCategories();
		//Get by Name
		public List<Category> GetCategoriesByName(string categoryName);
		//Get by Id
		public Category GetCategoryByID(int categoryId);
		#endregion

		#region UPDATE
		public int UpdateCategory(int categoryId,Category category);
		#endregion

		#region DELETE
		public void DeleteCategoryByID(int categoryId);

		#endregion




	}
}

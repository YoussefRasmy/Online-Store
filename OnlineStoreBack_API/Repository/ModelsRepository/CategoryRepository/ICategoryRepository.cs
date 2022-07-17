using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public interface ICategoryRepository:IGenericRepository<Category,int?>
	{
	
	

	
		//Get by Name
		public List<Category> GetByName(string categoryName);
		// Product Category (required, displays list of Sub‐categories only or Parent Categories which don’t have any sub‐categories underneath) What does this mean???
		public List<Category> GetSupAndParentOnly();

		public List<Category> GetParentCategoryOnly();






	}
}

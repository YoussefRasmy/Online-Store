using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository 
{
	public class CategoryRepository : GenericRepository<Category, int?>, ICategoryRepository
	{
		private readonly OnlineStoreContext db;

		public CategoryRepository(OnlineStoreContext context) : base(context)
		{
			this.db = context;
		}


		
		public List<Category> GetByName(string categoryName)
		{

			var res = db.Categories.Where(c => c.Name.Contains(categoryName)).ToList();
			return res;
		}

		public List<Category> GetParentCategoryOnly()
		{
			var res = db.Categories.Where(x => !db.Products.Select(c => c.CategoryId).Contains(x.Id)).ToList();
			
			return res;
		}
		// Product Category (required, displays list of Sub‐categories only or Parent Categories which don’t have sup category undernees 
		public List<Category> GetSupAndParentOnly()
		{
			var res = db.Categories.Where(x => !db.Categories.Select(c => c.ParentCategoryId).Contains(x.Id)).ToList();

			return res;

		}

	}
}

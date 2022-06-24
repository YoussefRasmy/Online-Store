using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[ForeignKey("ParentCategory")]
		public int? ParentCategoryId { get; set; }


		public virtual Category ParentCategory { get; set; }
	}
}

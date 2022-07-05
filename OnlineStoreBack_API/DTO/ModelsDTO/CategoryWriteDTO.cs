using System.ComponentModel.DataAnnotations;

namespace OnlineStoreBack_API.DTO.ModelsDTO
{
	public class CategoryWriteDTO
	{
		[Required]
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }

	}
}

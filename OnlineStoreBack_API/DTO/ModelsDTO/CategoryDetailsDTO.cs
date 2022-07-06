namespace OnlineStoreBack_API.DTO.ModelsDTO
{
	public class CategoryDetailsDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ParentCategoryName { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}

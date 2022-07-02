namespace OnlineStoreBack_API.DTO.ModelsDTO
{
	public class cartReadDTO
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public ICollection<ProductCartDTOOutput> Products { get; set; }




	}
}

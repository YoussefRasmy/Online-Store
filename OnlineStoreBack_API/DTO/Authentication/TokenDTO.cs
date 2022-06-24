namespace OnlineStoreBack_API.DTO.Authentication
{
	public class TokenDTO
	{
		public string Token { get; set; }
		public DateTime ExpiryDate { get; set; }
	}
}

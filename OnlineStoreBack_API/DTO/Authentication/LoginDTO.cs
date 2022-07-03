using System.ComponentModel.DataAnnotations;

namespace OnlineStoreBack_API.DTO.Authentication
{
	public class LoginDTO
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;

namespace OnlineStoreBack_API.DTO.Authentication
{
	public record RegisterDTO
	{
		[Required]
		public string Username { get; init; }
		[Required]
		public string Password { get; init; }
		[Required]
		public string Email { get; init; }
		[Required]
		public string Address { get; init; }
		[Required]
		public string FirstName { get; init; }
		[Required]
		public string LastName { get; init; }
		public string Title { get; init; }
	}
}

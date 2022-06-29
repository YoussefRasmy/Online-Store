using OnlineStoreBack_API.Data.Models;
using System.Security.Claims;

namespace OnlineStoreBack_API.Repository.Services
{
	public interface ICartService
	{
		public Task<Cart> GetUserCart(ClaimsPrincipal _user);
	}
}

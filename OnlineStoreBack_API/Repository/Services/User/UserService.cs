using Microsoft.AspNetCore.Identity;
using OnlineStoreBack_API.Data.Models;
using System.Linq;
using System.Security.Claims;

namespace OnlineStoreBack_API.Repository.Services.User
{
	public class UserService : IUserService
	{
		private readonly UserManager<StoreUser> userManager;

		public UserService(UserManager<StoreUser> userManager)
		{
			this.userManager = userManager;
		}
		public async Task< string> GetUserId(ClaimsPrincipal _user)
		{
			var userName = _user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

			var user = await userManager.FindByNameAsync(userName);
			var userId = await userManager.GetUserIdAsync(user);

			return userId;
		}





	}
}
